using System;

namespace fitcare.Models.Entities;

public class Base
{
	public Base() { }

	public Base(string creadoPor, DateTime creadoEl)
	{
		CreadoEl = creadoEl;
		CreadoPor = creadoPor;
	}

	public Base(string creadoPor, DateTime creadoEl, string editadoPor = null, DateTime? editadoEl = null)
	{
		CreadoEl = creadoEl;
		CreadoPor = creadoPor;
		EditadoEl = editadoEl;
		EditadoPor = editadoPor;
	}

	public DateTime CreadoEl { get; set; }
	public string CreadoPor { get; set; }
	public DateTime? EditadoEl { get; set; }
	public string EditadoPor { get; set; }
}
