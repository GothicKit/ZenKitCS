namespace ZenKit.Util
{
	public interface IMaterializing<out T>
	{
		/// <summary>
		///     Fully loads the underlying native object into a C# serializable object,
		///     disassociated from the underlying native implementation.
		/// </summary>
		/// <returns>The underlying native object in a pure C# representation.</returns>
		public T Materialize();
	}
}