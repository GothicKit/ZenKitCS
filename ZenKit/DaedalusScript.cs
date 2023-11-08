using System.Runtime.InteropServices;

namespace ZenKit;

public enum DaedalusDataType
{
	Void = 0,
	Float = 1,
	Int = 2,
	String = 3,
	Class = 4,
	Function = 5,
	Prototype = 6,
	Instance = 7
}

public enum DaedalusOpcode
{
	Add = 0,
	Sub = 1,
	Mul = 2,
	Div = 3,
	Mod = 4,
	Or = 5,
	Andb = 6,
	Lt = 7,
	Gt = 8,
	Movi = 9,
	Orr = 11,
	And = 12,
	Lsl = 13,
	Lsr = 14,
	Lte = 15,
	Eq = 16,
	Neq = 17,
	Gte = 18,
	AddMovi = 19,
	SubMovi = 20,
	MulMovi = 21,
	DivMovi = 22,
	Plus = 30,
	Negate = 31,
	Not = 32,
	Cmpl = 33,
	Nop = 45,
	Rsr = 60,
	Bl = 61,
	Be = 62,
	Pushi = 64,
	Pushv = 65,
	Pushvi = 67,
	Movs = 70,
	Movss = 71,
	Movvf = 72,
	Movf = 73,
	Movvi = 74,
	B = 75,
	Bz = 76,
	Gmovi = 80,
	Pushvv = 245
}

[StructLayout(LayoutKind.Explicit)]
public struct DaedalusInstruction
{
	[FieldOffset(0)] public DaedalusOpcode Opcode;

	[FieldOffset(4)] public byte size;

	[FieldOffset(8)] private uint opAddress;

	[FieldOffset(8)] private uint opSymbol;

	[FieldOffset(8)] private int opImmediateInt;

	[FieldOffset(8)] private float opImmediateFloat;

	[FieldOffset(12)] public byte opIndex;
}

public class DaedalusInstance
{
	public DaedalusInstance(UIntPtr handle)
	{
		Handle = handle;
	}

	internal UIntPtr Handle { get; }
}

public class DaedalusSymbol
{
	private readonly UIntPtr _handle;

	public DaedalusSymbol(UIntPtr handle)
	{
		_handle = handle;
	}

	public bool IsConst => Native.ZkDaedalusSymbol_getIsConst(_handle);
	public bool IsMember => Native.ZkDaedalusSymbol_getIsMember(_handle);
	public bool IsExternal => Native.ZkDaedalusSymbol_getIsExternal(_handle);
	public bool IsMerged => Native.ZkDaedalusSymbol_getIsMerged(_handle);
	public bool IsGenerated => Native.ZkDaedalusSymbol_getIsGenerated(_handle);
	public bool HasReturn => Native.ZkDaedalusSymbol_getHasReturn(_handle);
	public string Name => Native.ZkDaedalusSymbol_getName(_handle).MarshalAsString() ?? string.Empty;
	public int Address => Native.ZkDaedalusSymbol_getAddress(_handle);
	public int Parent => Native.ZkDaedalusSymbol_getParent(_handle);
	public int Size => Native.ZkDaedalusSymbol_getSize(_handle);
	public DaedalusDataType Type => Native.ZkDaedalusSymbol_getType(_handle);
	public uint Index => Native.ZkDaedalusSymbol_getIndex(_handle);
	public DaedalusDataType ReturnType => Native.ZkDaedalusSymbol_getReturnType(_handle);

	public string GetString(ushort index, DaedalusInstance? context = null)
	{
		return Native.ZkDaedalusSymbol_getString(_handle, index, context?.Handle ?? UIntPtr.Zero).MarshalAsString() ??
		       string.Empty;
	}

	public float GetFloat(ushort index, DaedalusInstance? context = null)
	{
		return Native.ZkDaedalusSymbol_getFloat(_handle, index, context?.Handle ?? UIntPtr.Zero);
	}

	public int GetInt(ushort index, DaedalusInstance? context = null)
	{
		return Native.ZkDaedalusSymbol_getInt(_handle, index, context?.Handle ?? UIntPtr.Zero);
	}

	public void SetString(string value, ushort index, DaedalusInstance? context = null)
	{
		Native.ZkDaedalusSymbol_setString(_handle, value, index, context?.Handle ?? UIntPtr.Zero);
	}

	public void SetFloat(float value, ushort index, DaedalusInstance? context = null)
	{
		Native.ZkDaedalusSymbol_setFloat(_handle, value, index, context?.Handle ?? UIntPtr.Zero);
	}

	public void SetInt(int value, ushort index, DaedalusInstance? context = null)
	{
		Native.ZkDaedalusSymbol_setInt(_handle, value, index, context?.Handle ?? UIntPtr.Zero);
	}
}

public class DaedalusScript
{
	private readonly UIntPtr _handle;

	public DaedalusScript(string path)
	{
		_handle = Native.ZkDaedalusScript_loadPath(path);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusScript");
	}

	public DaedalusScript(Read r)
	{
		_handle = Native.ZkDaedalusScript_load(r.Handle);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusScript");
	}

	public DaedalusScript(Vfs vfs, string name)
	{
		_handle = Native.ZkDaedalusScript_loadVfs(vfs.Handle, name);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusScript");
	}

	public uint SymbolCount => Native.ZkDaedalusScript_getSymbolCount(_handle);

	public List<DaedalusSymbol> Symbols
	{
		get
		{
			var symbols = new List<DaedalusSymbol>();

			Native.ZkDaedalusScript_enumerateSymbols(_handle, (_, symbol) =>
			{
				symbols.Add(new DaedalusSymbol(symbol));
				return false;
			}, UIntPtr.Zero);

			return symbols;
		}
	}

	~DaedalusScript()
	{
		Native.ZkDaedalusScript_del(_handle);
	}

	public List<DaedalusSymbol> GetInstanceSymbols(string className)
	{
		var symbols = new List<DaedalusSymbol>();

		Native.ZkDaedalusScript_enumerateInstanceSymbols(_handle, className, (_, symbol) =>
		{
			symbols.Add(new DaedalusSymbol(symbol));
			return false;
		}, UIntPtr.Zero);

		return symbols;
	}

	public DaedalusInstruction GetInstruction(ulong address)
	{
		return Native.ZkDaedalusScript_getInstruction(_handle, address);
	}

	public DaedalusSymbol? GetSymbolByIndex(uint index)
	{
		var sym = Native.ZkDaedalusScript_getSymbolByIndex(_handle, index);
		return sym == UIntPtr.Zero ? null : new DaedalusSymbol(sym);
	}

	public DaedalusSymbol? GetSymbolByAddress(ulong address)
	{
		var sym = Native.ZkDaedalusScript_getSymbolByAddress(_handle, address);
		return sym == UIntPtr.Zero ? null : new DaedalusSymbol(sym);
	}

	public DaedalusSymbol? GetSymbolByName(string name)
	{
		var sym = Native.ZkDaedalusScript_getSymbolByName(_handle, name);
		return sym == UIntPtr.Zero ? null : new DaedalusSymbol(sym);
	}
}