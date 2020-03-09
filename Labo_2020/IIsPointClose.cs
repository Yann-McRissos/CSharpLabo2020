namespace MyCartographyObjects
{
	public interface IIsPointClose
	{
		bool IsPointClose(double latitude, double longitude, double precision);
	}
}
