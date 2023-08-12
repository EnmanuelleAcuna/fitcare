using System;

namespace fitcare.Models.Extras;

public static class Factory
{
	public static Guid NewGUID(Guid id)
	{
		Guid idObjeto = id.Equals(new Guid()) ? Guid.NewGuid() : id;
		return idObjeto;
	}

	public static Guid NewGUID(string id)
	{
		Guid idObjeto = Guid.NewGuid();

		if (string.IsNullOrEmpty(id))
		{
			return idObjeto;
		}

		if (Guid.TryParse(id, out idObjeto))
		{
			return idObjeto;
		}
		else
		{
			return idObjeto;
		}
	}

	public static DateTime NewDateTime(DateTime dateTime)
	{
		DateTime date = dateTime.Equals(DateTime.MinValue) ? DateTime.Now : dateTime;
		return date;
	}
}
