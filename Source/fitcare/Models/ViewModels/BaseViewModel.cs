using fitcare.Models.Entities;

namespace fitcare.Models.ViewModels;

public class BaseViewModel
{
	public BaseViewModel() { }

	public BaseViewModel(Base baseModel)
	{
		CreadoPor = baseModel.CreadoPor;
		CreadoEl = baseModel.CreadoEl.ToString("dd/MM/yyyy");
		EditadoPor = baseModel.EditadoPor;
		EditadoEl = baseModel.EditadoEl.HasValue ? baseModel.EditadoEl.Value.ToString("dd/MM/yyyy") : string.Empty;
	}

	public string CreadoPor { get; private set; }
	public string CreadoEl { get; private set; }
	public string EditadoPor { get; private set; } = string.Empty;
	public string EditadoEl { get; private set; } = string.Empty;
}
