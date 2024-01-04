namespace ZenKit.Util
{
	public class Matrix3x3
	{
		public float M11, M12, M13, M21, M22, M23, M31, M32, M33;

		public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32,
			float m33)
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M21 = m21;
			M22 = m22;
			M23 = m23;
			M31 = m31;
			M32 = m32;
			M33 = m33;
		}
	}
}