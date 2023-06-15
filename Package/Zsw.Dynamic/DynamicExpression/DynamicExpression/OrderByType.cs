namespace Zsw.Dynamic
{
	public enum OrderByType
	{
		[OrderByTypeDescriptor(MethodMap = "OrderBy")]
		Asc,
		[OrderByTypeDescriptor(MethodMap = "OrderByDescending")]
		Desc
	}
}
