using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZenKit.Util;
using ZenKit.Vobs;

namespace ZenKit
{
    /// <summary>A single cutscene message.</summary>
    public interface ICutsceneMessage : ICacheable<ICutsceneMessage>
    {
        public int Type { get; set; }

        /// <summary>The text associated with the message.</summary>
        /// <remarks>If the message cannot be loaded from the native interface for any reason an empty text will be returned.</remarks>
        public string Text { get; set; }

        /// <summary>The name of the <tt>WAV</tt> file containing the message's audio. If the message</summary>
        /// <remarks>If the message cannot be loaded from the native interface for any reason an empty text will be returned.</remarks>
        public string Name { get; set; }

        public bool HighPriority { get; set; }
        public bool Used { get; set; }
        public bool Deleted { get; set; }
    }

    /// <summary>A cached <see cref="CutsceneMessage" />.</summary>
    [Serializable]
    public class CachedCutsceneMessage : ICutsceneMessage
    {
        /// <inheritdoc />
        public int Type { get; set; }

        /// <inheritdoc />
        public string Text { get; set; } = "";

        /// <inheritdoc />
        public string Name { get; set; } = "";

        public bool HighPriority { get; set; } = false;
        public bool Used { get; set; } = false;
        public bool Deleted { get; set; } = false;

        /// <inheritdoc />
        public ICutsceneMessage Cache()
        {
            return this;
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return true;
        }
    }

    /// <inheritdoc />
    public class CutsceneMessage : ICutsceneMessage
    {
        internal UIntPtr Handle { get; }

        internal CutsceneMessage(UIntPtr handle)
        {
            Handle = handle;
        }

        /// <inheritdoc />
        public int Type
        {
            get => (int)Native.ZkCutsceneMessage_getType(Handle);
            set => Native.ZkCutsceneMessage_setType(Handle, (uint)value);
        }

        /// <inheritdoc />
        public string Text
        {
            get => Native.ZkCutsceneMessage_getText(Handle).MarshalAsString();
            set => Native.ZkCutsceneMessage_setText(Handle, value);
        }

        /// <inheritdoc />
        public string Name
        {
            get => Native.ZkCutsceneMessage_getName(Handle).MarshalAsString();
            set => Native.ZkCutsceneMessage_setName(Handle, value);
        }

        public bool HighPriority
        {
            get => Native.ZkCutsceneMessage_getIsHighPriority(Handle);
            set => Native.ZkCutsceneMessage_setIsHighPriority(Handle, value);
        }

        public bool Used
        {
            get => Native.ZkCutsceneMessage_getIsUsed(Handle);
            set => Native.ZkCutsceneMessage_setIsUsed(Handle, value);
        }

        public bool Deleted
        {
            get => Native.ZkCutsceneMessage_getIsDeleted(Handle);
            set => Native.ZkCutsceneMessage_setIsDeleted(Handle, value);
        }

        /// <inheritdoc />
        public ICutsceneMessage Cache()
        {
            return new CachedCutsceneMessage
            {
                Type = Type,
                Text = Text,
                Name = Name
            };
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return false;
        }

        ~CutsceneMessage()
        {
            Native.ZkCutsceneMessage_release(Handle);
        }
    }

    /// <summary>A block containing a cutscene message and a unique name.</summary>
    public interface ICutsceneBlock : ICacheable<ICutsceneBlock>
    {
        /// <summary>The unique name of the message</summary>
        public string Name { get; }

        /// <summary>The content of the message.</summary>
        /// <remarks>
        ///     It seems like it was at one point possible to specify multiple <see cref="CutsceneMessage" />
        ///     objects for each <see cref="CutsceneBlock" />. This seems to have been abandoned, however,
        ///     so this implementation only supports one <see cref="CutsceneMessage" /> per message block.
        /// </remarks>
        public ICutsceneMessage Message { get; set; }
    }

    /// <summary>A cached <see cref="CutsceneBlock" />.</summary>
    [Serializable]
    public class CachedCutsceneBlock : ICutsceneBlock
    {
        /// <inheritdoc />
        public string Name { get; set; } = "";

        /// <inheritdoc />
        public ICutsceneMessage Message { get; set; }

        /// <inheritdoc />
        public ICutsceneBlock Cache()
        {
            return this;
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return true;
        }
    }

    /// <inheritdoc />
    public class CutsceneBlock : ICutsceneBlock
    {
        internal readonly UIntPtr Handle;

        internal CutsceneBlock(UIntPtr handle)
        {
            Handle = handle;
        }

        /// <inheritdoc />
        public string Name => Native.ZkCutsceneBlock_getName(Handle).MarshalAsString();

        /// <inheritdoc />
        /// <exception cref="NativeAccessError">The native backend failed to return a valid value.</exception>
        public ICutsceneMessage Message
        {
            get
            {
                var handle = Native.ZkCutsceneBlock_getMessage(Handle);
                if (handle == UIntPtr.Zero) throw new NativeAccessError("CutsceneBlock.Message");
                return new CutsceneMessage(handle);
            }
            set
            {
                if (value.IsCached())
                    throw new NotImplementedException("CutsceneBlock.Message setter with cached message");
                var native = (CutsceneMessage)value;
                Native.ZkCutsceneBlock_setMessage(Handle, native.Handle);
            }
        }

        /// <inheritdoc />
        public ICutsceneBlock Cache()
        {
            return new CachedCutsceneBlock
            {
                Name = Name,
                Message = Message.Cache()
            };
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return false;
        }

        protected virtual void Delete()
        {
            Native.ZkCutsceneBlock_release(Handle);
        }

        ~CutsceneBlock()
        {
            Delete();
        }
    }

    /// <summary>
    ///     Represents a cutscene library.
    ///     <para>
    ///         Cutscene libraries, also called message databases, contain voice lines and a reference to the associated
    ///         audio recording of voice actors. These files are used in conjunction with scripts to facilitate PC to NPC
    ///         conversations in-game. Cutscene libraries are found within the <c>_work/data/scripts/content/cutscene/</c>
    ///         directory of Gothic and Gothic II installations. Cutscene libraries are ZenGin archives in either binary
    ///         or ASCII format. They have either the <c>.DAT</c> or <c>.BIN</c> extension for binary files or the
    ///         <c>.CSL</c> or <c>.LSC</c> extension for text files.
    ///     </para>
    /// </summary>
    public interface ICutsceneLibrary : ICacheable<ICutsceneLibrary>
    {
        /// <summary>A list of all message blocks in the database.</summary>
        public List<ICutsceneBlock> Blocks { get; }

        /// <summary>Retrieves a message block by it's name.</summary>
        /// <param name="name">The name of the block to get</param>
        /// <returns>A pointer to the block or <c>null</c> if the block was not found.</returns>
        public ICutsceneBlock? GetBlock(string name);
    }

    /// <summary>A cached <see cref="CutsceneLibrary" />.</summary>
    [Serializable]
    public struct CachedCutsceneLibrary : ICutsceneLibrary
    {
        /// <inheritdoc />
        public List<ICutsceneBlock> Blocks { get; set; }

        /// <inheritdoc />
        public ICutsceneBlock? GetBlock(string name)
        {
            return Blocks.FirstOrDefault(block => block.Name == name);
        }

        /// <inheritdoc />
        public ICutsceneLibrary Cache()
        {
            return this;
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return true;
        }
    }

    /// <inheritdoc />
    public class CutsceneLibrary : ICutsceneLibrary
    {
        private readonly UIntPtr _handle;

        /// <summary>Load a cutscene library from a file.</summary>
        /// <param name="path">The path to the file to load.</param>
        /// <exception cref="IOException">The file loading/parsing failed.</exception>
        public CutsceneLibrary(string path)
        {
            _handle = Native.ZkCutsceneLibrary_loadPath(path);
            if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
        }

        /// <summary>Load a cutscene library from an input stream.</summary>
        /// <param name="r">The stream to read from..</param>
        /// <exception cref="IOException">The file loading/parsing failed.</exception>
        public CutsceneLibrary(Read r)
        {
            _handle = Native.ZkCutsceneLibrary_load(r.Handle);
            if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
        }

        /// <summary>Load a cutscene library from a VFS.</summary>
        /// <param name="vfs">VFS to load the file from.</param>
        /// <param name="name">The name of the file to load from the given <paramref name="vfs" />.</param>
        /// <exception cref="IOException">The file loading/parsing failed.</exception>
        public CutsceneLibrary(Vfs vfs, string name)
        {
            _handle = Native.ZkCutsceneLibrary_loadVfs(vfs.Handle, name);
            if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
        }

        /// <inheritdoc />
        public List<ICutsceneBlock> Blocks
        {
            get
            {
                var blocks = new List<ICutsceneBlock>();
                var count = (int)Native.ZkCutsceneLibrary_getBlockCount(_handle);
                for (var i = 0; i < count; ++i)
                    blocks.Add(new CutsceneBlock(Native.ZkCutsceneLibrary_getBlockByIndex(_handle, (ulong)i)));
                return blocks;
            }
        }

        /// <inheritdoc />
        public ICutsceneLibrary Cache()
        {
            return new CachedCutsceneLibrary
            {
                Blocks = Blocks.ConvertAll(block => block.Cache())
            };
        }

        /// <inheritdoc />
        public bool IsCached()
        {
            return false;
        }

        /// <inheritdoc />
        public ICutsceneBlock? GetBlock(string name)
        {
            var block = Native.ZkCutsceneLibrary_getBlock(_handle, name);
            return block == UIntPtr.Zero ? null : new CutsceneBlock(block);
        }

        ~CutsceneLibrary()
        {
            Native.ZkCutsceneLibrary_del(_handle);
        }
    }

    public interface ICutsceneProps
    {
        public string Name { get; set; }

        public bool IsGlobal { get; set; }

        public bool IsLoop { get; set; }

        public bool HasToBeTriggered { get; set; }

        public float Distance { get; set; }

        public float Range { get; set; }

        public int LockedBlockCount { get; set; }

        public uint RunBehaviour { get; set; }

        public int RunBehaviourValue { get; set; }

        public string StageName { get; set; }

        public string ScriptFunctionOnStop { get; set; }
    }

    public class CutsceneProps : ICutsceneProps
    {
        internal readonly UIntPtr Handle;

        public CutsceneProps()
        {
            Handle = Native.ZkCutsceneProps_new();
        }

        internal CutsceneProps(UIntPtr handle)
        {
            Handle = handle;
        }

        public string Name
        {
            get => Native.ZkCutsceneProps_getName(Handle).MarshalAsString();
            set => Native.ZkCutsceneProps_setName(Handle, value);
        }

        public bool IsGlobal
        {
            get => Native.ZkCutsceneProps_getIsGlobal(Handle);
            set => Native.ZkCutsceneProps_setIsGlobal(Handle, value);
        }

        public bool IsLoop
        {
            get => Native.ZkCutsceneProps_getIsLoop(Handle);
            set => Native.ZkCutsceneProps_setIsLoop(Handle, value);
        }

        public bool HasToBeTriggered
        {
            get => Native.ZkCutsceneProps_getHasToBeTriggered(Handle);
            set => Native.ZkCutsceneProps_setHasToBeTriggered(Handle, value);
        }

        public float Distance
        {
            get => Native.ZkCutsceneProps_getDistance(Handle);
            set => Native.ZkCutsceneProps_setDistance(Handle, value);
        }

        public float Range
        {
            get => Native.ZkCutsceneProps_getRange(Handle);
            set => Native.ZkCutsceneProps_setRange(Handle, value);
        }

        public int LockedBlockCount
        {
            get => Native.ZkCutsceneProps_getLockedBlockCount(Handle);
            set => Native.ZkCutsceneProps_setLockedBlockCount(Handle, value);
        }

        public uint RunBehaviour
        {
            get => Native.ZkCutsceneProps_getRunBehaviour(Handle);
            set => Native.ZkCutsceneProps_setRunBehaviour(Handle, value);
        }

        public int RunBehaviourValue
        {
            get => Native.ZkCutsceneProps_getRunBehaviourValue(Handle);
            set => Native.ZkCutsceneProps_setRunBehaviourValue(Handle, value);
        }

        public string StageName
        {
            get => Native.ZkCutsceneProps_getStageName(Handle).MarshalAsString();
            set => Native.ZkCutsceneProps_setStageName(Handle, value);
        }

        public string ScriptFunctionOnStop
        {
            get => Native.ZkCutsceneProps_getScriptFunctionOnStop(Handle).MarshalAsString();
            set => Native.ZkCutsceneProps_setScriptFunctionOnStop(Handle, value);
        }

        ~CutsceneProps()
        {
            Native.ZkCutsceneProps_release(Handle);
        }
    }

    public interface ICutsceneContext : ICutsceneBlock
    {
        public CutsceneProps? Props { get; set; }
        public int RoleCount { get; set; }
        public int RoleVobCount { get; set; }
        public Npc? Npc { get; set; }
        public Npc? MainRole { get; set; }
        public bool IsCutscene { get; set; }
        public int Reference { get; set; }
        public int ActualBlock { get; set; }
        public bool WasTriggered { get; set; }
    }

    public class CutsceneContext : CutsceneBlock, ICutsceneContext
    {
        public CutsceneContext() : base(Native.ZkCutsceneContext_new())
        {
        }

        internal CutsceneContext(UIntPtr handle) : base(handle)
        {
        }

        public CutsceneProps? Props
        {
            get
            {
                var handle = Native.ZkCutsceneContext_getProps(Handle);
                return handle == UIntPtr.Zero ? null : new CutsceneProps(handle);
            }
            set => Native.ZkCutsceneContext_setProps(Handle, value?.Handle ?? UIntPtr.Zero);
        }

        public int RoleCount
        {
            get => Native.ZkCutsceneContext_getRoleCount(Handle);
            set => Native.ZkCutsceneContext_setRoleCount(Handle, value);
        }

        public int RoleVobCount
        {
            get => Native.ZkCutsceneContext_getRoleVobCount(Handle);
            set => Native.ZkCutsceneContext_setRoleVobCount(Handle, value);
        }

        public Npc? Npc
        {
            get
            {
                var handle = Native.ZkCutsceneContext_getNpc(Handle);
                return handle == UIntPtr.Zero ? null : new Npc(handle);
            }
            set => Native.ZkCutsceneContext_setNpc(Handle, value?.Handle ?? UIntPtr.Zero);
        }

        public Npc? MainRole
        {
            get
            {
                var handle = Native.ZkCutsceneContext_getMainRole(Handle);
                return handle == UIntPtr.Zero ? null : new Npc(handle);
            }
            set => Native.ZkCutsceneContext_setMainRole(Handle, value?.Handle ?? UIntPtr.Zero);
        }

        public bool IsCutscene
        {
            get => Native.ZkCutsceneContext_getIsCutscene(Handle);
            set => Native.ZkCutsceneContext_setIsCutscene(Handle, value);
        }

        public int Reference
        {
            get => Native.ZkCutsceneContext_getReference(Handle);
            set => Native.ZkCutsceneContext_setReference(Handle, value);
        }

        public int ActualBlock
        {
            get => Native.ZkCutsceneContext_getActualBlock(Handle);
            set => Native.ZkCutsceneContext_setActualBlock(Handle, value);
        }

        public bool WasTriggered
        {
            get => Native.ZkCutsceneContext_getWasTriggered(Handle);
            set => Native.ZkCutsceneContext_setWasTriggered(Handle, value);
        }

        protected override void Delete()
        {
            Native.ZkCutsceneContext_release(Handle);
        }
    }
}