namespace ZenKit.Util
{
	public interface ICacheable<out T>
	{
		/// <summary>
		///     Fully loads the underlying native object into a C# serializable object,
		///     disassociated from the underlying native implementation.
		/// </summary>
		/// <returns>The underlying native object in a pure C# representation.</returns>
		public T Cache();

		/// <summary>
		///     Tests whether this cacheable object is actually cached.
		/// </summary>
		/// <returns>true, if this object is cached and false if not.</returns>
		public bool IsCached();
	}
}