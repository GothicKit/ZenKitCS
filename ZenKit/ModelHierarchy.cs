using System.Numerics;
using System.Runtime.InteropServices;

namespace ZenKit;

[StructLayout(LayoutKind.Sequential)]
public struct ModelHierarchyNode
{
	public short ParentIndex;
	public IntPtr _name;
	private Native.ZkMat4x4 _transform;

	public string Name => _name.MarshalAsString() ?? throw new Exception("Failed to load model hierarchy node name");

	public Matrix4x4 Transform =>
		new(
			_transform.m00,
			_transform.m10,
			_transform.m20,
			_transform.m30,
			_transform.m01,
			_transform.m11,
			_transform.m21,
			_transform.m31,
			_transform.m02,
			_transform.m12,
			_transform.m22,
			_transform.m32,
			_transform.m03,
			_transform.m13,
			_transform.m23,
			_transform.m33
		);
}

public class ModelHierarchy
{
	private readonly bool _delete = true;
	private readonly UIntPtr _handle;

	public ModelHierarchy(string path)
	{
		_handle = Native.ZkModelHierarchy_loadPath(path);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
	}

	public ModelHierarchy(Read buf)
	{
		_handle = Native.ZkModelHierarchy_load(buf.Handle);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
	}

	public ModelHierarchy(Vfs vfs, string name)
	{
		_handle = Native.ZkModelHierarchy_loadVfs(vfs.Handle, name);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
	}

	public ModelHierarchy(UIntPtr handle)
	{
		_handle = handle;
		_delete = false;
	}

	public ulong NodeCount => Native.ZkModelHierarchy_getNodeCount(_handle);
	public AxisAlignedBoundingBox BoundingBox => Native.ZkModelHierarchy_getBbox(_handle);
	public AxisAlignedBoundingBox CollisionBoundingBox => Native.ZkModelHierarchy_getCollisionBbox(_handle);
	public Vector3 RootTranslation => Native.ZkModelHierarchy_getRootTranslation(_handle);
	public uint Checksum => Native.ZkModelHierarchy_getChecksum(_handle);
	public DateTime SourceDate => Native.ZkModelHierarchy_getSourceDate(_handle).AsDateTime();

	public string SourcePath => Native.ZkModelHierarchy_getSourcePath(_handle).MarshalAsString() ??
	                            throw new Exception("Failed to load model hierarchy source path");

	public List<ModelHierarchyNode> Nodes
	{
		get
		{
			var nodes = new List<ModelHierarchyNode>();

			Native.ZkModelHierarchy_enumerateNodes(_handle, (_, node) =>
			{
				nodes.Add(Marshal.PtrToStructure<ModelHierarchyNode>(node));
				return false;
			}, UIntPtr.Zero);

			return nodes;
		}
	}

	~ModelHierarchy()
	{
		if (_delete) Native.ZkModelHierarchy_del(_handle);
	}

	public ModelHierarchyNode GetNode(ulong i)
	{
		return Native.ZkModelHierarchy_getNode(_handle, i);
	}
}