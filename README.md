/* Respuesta al Challenge:

Soporte para una nueva forma geométrica: Trapecio
Se agregó la constante public const int Trapecio = 4
Se agregó un constructor específico para Trapecio
Valida que el tipo sea Trapecio.
Guarda base mayor, base menor y altura en campos privados.
Se implementó el cálculo de área del trapecio
Se implementó el cálculo del perímetro del trapecio (asumiendo trapecio isósceles)

Soporte para un nuevo idioma: Italiano
Se agregó la constante public const int Italiano = 3;.
Se extendieron los textos del reporte para el idioma italiano:
Título: <h1>Rapporto delle forme</h1>
Pie de reporte: forme Perimetro ... Area ...
Se extendió el método TraducirForma con los nombres de formas en italiano:
Quadrato, Cerchio, Triangolo, Trapezio (singulares)
Quadrati, Cerchi, Triangoli, Trapezi (plurales)

Ajustes en el método Imprimir:
Se extendió la lógica para:
Acumular cantidad, área y perímetro de trapecios.
Generar líneas de salida para trapecios.

Nuevas pruebas unitarias:
Se agregaron 2 nuevos tests en DataTests.cs:
TestResumenListaConTrapeciosEnCastellano: prueba un reporte de trapecios en castellano.
TestResumenListaConFormasEnItaliano: prueba un reporte mixto (trapecio, cuadrado, círculo) en italiano.

*/
