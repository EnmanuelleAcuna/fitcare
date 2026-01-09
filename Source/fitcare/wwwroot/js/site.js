// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// ============================================================================
// Cascading Dropdowns (Province → Canton → District)
// ============================================================================
/**
 * Inicializa dropdowns en cascada para Provincia → Cantón → Distrito
 * @param {Object} config - Configuración del componente
 * @param {string} config.provinciaSelector - Selector del dropdown de provincia
 * @param {string} config.cantonSelector - Selector del dropdown de cantón
 * @param {string} config.distritoSelector - Selector del dropdown de distrito
 * @param {string} config.cantonesUrl - URL para obtener cantones por provincia
 * @param {string} config.distritosUrl - URL para obtener distritos por cantón
 */
function initCascadingDropdowns(config) {
	var $provincia = $(config.provinciaSelector);
	var $canton = $(config.cantonSelector);
	var $distrito = $(config.distritoSelector);

	$provincia.on('change', function () {
		var idProvincia = $(this).val();

		// Limpiar y deshabilitar cantones y distritos
		$canton.empty().append('<option value="">Cargando...</option>').prop('disabled', true);
		$distrito.empty().append('<option value="">Seleccione primero un cantón</option>').prop('disabled', true);

		if (idProvincia) {
			$.ajax({
				url: config.cantonesUrl,
				type: 'GET',
				data: { idProvincia: idProvincia },
				success: function (data) {
					$canton.empty().append('<option value="">Seleccione un cantón</option>');

					if (data && data.length > 0) {
						$.each(data, function (index, canton) {
							$canton.append($('<option></option>').val(canton.value).text(canton.text));
						});
						$canton.prop('disabled', false);
					} else {
						$canton.append('<option value="">No hay cantones disponibles</option>');
					}
				},
				error: function () {
					$canton.empty().append('<option value="">Error al cargar cantones</option>');
				}
			});
		} else {
			$canton.empty().append('<option value="">Seleccione primero una provincia</option>');
		}
	});

	$canton.on('change', function () {
		var idCanton = $(this).val();

		// Limpiar y deshabilitar distritos
		$distrito.empty().append('<option value="">Cargando...</option>').prop('disabled', true);

		if (idCanton) {
			$.ajax({
				url: config.distritosUrl,
				type: 'GET',
				data: { idCanton: idCanton },
				success: function (data) {
					$distrito.empty().append('<option value="">Seleccione un distrito</option>');

					if (data && data.length > 0) {
						$.each(data, function (index, distrito) {
							$distrito.append($('<option></option>').val(distrito.value).text(distrito.text));
						});
						$distrito.prop('disabled', false);
					} else {
						$distrito.append('<option value="">No hay distritos disponibles</option>');
					}
				},
				error: function () {
					$distrito.empty().append('<option value="">Error al cargar distritos</option>');
				}
			});
		} else {
			$distrito.empty().append('<option value="">Seleccione primero un cantón</option>');
		}
	});
}

// ============================================================================
// Modal Confirmation with AJAX
// ============================================================================
/**
 * Inicializa un modal de confirmación con acción AJAX
 * @param {Object} config - Configuración del componente
 * @param {string} config.modalSelector - Selector del modal
 * @param {string} config.nameDisplaySelector - Selector donde mostrar el nombre de la entidad
 * @param {string} config.confirmButtonSelector - Selector del botón de confirmación
 * @param {string} config.actionUrl - URL de la acción AJAX
 * @param {string} config.errorMessage - Mensaje de error por defecto
 */
function initModalConfirmation(config) {
	var entityId = '';

	$(config.modalSelector).on('show.bs.modal', function (event) {
		var button = $(event.relatedTarget);
		entityId = button.data('id');
		var nombre = button.data('nombre');
		$(config.nameDisplaySelector).text(nombre);
	});

	$(config.confirmButtonSelector).on('click', function () {
		$.ajax({
			url: config.actionUrl,
			type: 'POST',
			contentType: 'application/json',
			data: JSON.stringify({ id: entityId }),
			success: function (data) {
				if (data.success) {
					window.location.reload();
				} else {
					alert(data.message || config.errorMessage || 'Error al procesar la solicitud');
				}
			},
			error: function () {
				alert('Error al procesar la solicitud');
			}
		});
	});
}

// ============================================================================
// DataTables Spanish Configuration
// ============================================================================
/**
 * Configuración base para DataTables en español
 */
var dataTablesSpanishConfig = {
	language: {
		url: "//cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
	},
	processing: true,
	pageLength: 10,
	lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]]
};

// ============================================================================
// Toast Notifications
// ============================================================================

//const menuToggle = document.querySelector('.menu-button');
//const menu = document.querySelector('.menu-options');

//menuToggle.addEventListener('click', () => {
//	menu.classList.toggle('opacity-0');
//});

//function saveProfile(req, res) {
//	// Find user by email
//}

//module.exports {
//	saveProfile
//};

/**
 * Función global para mostrar notificaciones Toast usando Bootstrap 4
 * @param {string} mensaje - El mensaje a mostrar en el toast
 * @param {string} tipo - El tipo de toast: 'success', 'danger', 'warning', 'info' (default: 'success')
 */
function mostrarToast(mensaje, tipo = 'success') {
	// Tipos disponibles: success, danger, warning, info
	var iconos = {
		success: 'fa-check-circle',
		danger: 'fa-exclamation-circle',
		warning: 'fa-exclamation-triangle',
		info: 'fa-info-circle'
	};

	var colores = {
		success: 'bg-success',
		danger: 'bg-danger',
		warning: 'bg-warning',
		info: 'bg-info'
	};

	var titulos = {
		success: 'Éxito',
		danger: 'Error',
		warning: 'Advertencia',
		info: 'Información'
	};

	var toastHtml = `
		<div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="4000">
			<div class="toast-header ${colores[tipo]} text-white">
				<i class="fas ${iconos[tipo]} mr-2"></i>
				<strong class="mr-auto">${titulos[tipo]}</strong>
				<button type="button" class="ml-2 mb-1 close text-white" data-dismiss="toast" aria-label="Close">
					<span aria-hidden="true">&times;</span>
				</button>
			</div>
			<div class="toast-body">
				${mensaje}
			</div>
		</div>
	`;

	var $toast = $(toastHtml);
	$('#toastContainer').append($toast);
	$toast.toast('show');

	// Eliminar del DOM después de ocultarse
	$toast.on('hidden.bs.toast', function () {
		$(this).remove();
	});
}

/**
 * Función para mostrar notificaciones desde TempData
 * Llama a mostrarToast si el mensaje no es nulo/vacío
 * @param {string} mensaje - El mensaje desde TempData["ToastMessage"]
 * @param {string} tipo - El tipo desde TempData["ToastType"]
 */
function mostrarNotificacion(mensaje, tipo) {
	if (mensaje && mensaje.trim() !== '') {
		mostrarToast(mensaje, tipo || 'success');
	}
}
