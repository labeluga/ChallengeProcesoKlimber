/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        #region Formas
        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;
        #endregion

        #region Idiomas
        public const int Castellano = 1;
        public const int Ingles = 2;
        public const int Italiano = 3;
        #endregion

        private readonly decimal _lado;
        private readonly decimal _lado2; // Para Trapecio (base menor)
        private readonly decimal _altura; // Para Trapecio

        public int Tipo { get; set; }

        // Constructor para Cuadrado, Circulo, TrianguloEquilatero (un solo lado)
        public FormaGeometrica(int tipo, decimal ancho)
        {
            Tipo = tipo;
            _lado = ancho;
        }

        // Constructor para Trapecio con base mayor, base menor y altura
        public FormaGeometrica(int tipo, decimal baseMayor, decimal baseMenor, decimal altura)
        {
            if (tipo != Trapecio)
                throw new ArgumentException("Este constructor es sólo para Trapecio.");

            Tipo = tipo;
            _lado = baseMayor;
            _lado2 = baseMenor;
            _altura = altura;
        }

        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            var sb = new StringBuilder();

            if (formas == null || !formas.Any())
            {
                if (idioma == Castellano)
                    sb.Append("<h1>Lista vacía de formas!</h1>");
                else if (idioma == Italiano)
                    sb.Append("<h1>Lista vuota di forme!</h1>");
                else
                    sb.Append("<h1>Empty list of shapes!</h1>");
                return sb.ToString();
            }

            // HEADER
            switch (idioma)
            {
                case Castellano:
                    sb.Append("<h1>Reporte de Formas</h1>");
                    break;
                case Italiano:
                    sb.Append("<h1>Rapporto delle forme</h1>");
                    break;
                case Ingles:
                default:
                    sb.Append("<h1>Shapes report</h1>");
                    break;
            }

            int numeroCuadrados = 0, numeroCirculos = 0, numeroTriangulos = 0, numeroTrapecios = 0;
            decimal areaCuadrados = 0m, areaCirculos = 0m, areaTriangulos = 0m, areaTrapecios = 0m;
            decimal perimetroCuadrados = 0m, perimetroCirculos = 0m, perimetroTriangulos = 0m, perimetroTrapecios = 0m;

            foreach (var forma in formas)
            {
                switch (forma.Tipo)
                {
                    case Cuadrado:
                        numeroCuadrados++;
                        areaCuadrados += forma.CalcularArea();
                        perimetroCuadrados += forma.CalcularPerimetro();
                        break;
                    case Circulo:
                        numeroCirculos++;
                        areaCirculos += forma.CalcularArea();
                        perimetroCirculos += forma.CalcularPerimetro();
                        break;
                    case TrianguloEquilatero:
                        numeroTriangulos++;
                        areaTriangulos += forma.CalcularArea();
                        perimetroTriangulos += forma.CalcularPerimetro();
                        break;
                    case Trapecio:
                        numeroTrapecios++;
                        areaTrapecios += forma.CalcularArea();
                        perimetroTrapecios += forma.CalcularPerimetro();
                        break;
                }
            }

            sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma));
            sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma));
            sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma));
            sb.Append(ObtenerLinea(numeroTrapecios, areaTrapecios, perimetroTrapecios, Trapecio, idioma));

            // FOOTER
            int totalFormas = numeroCuadrados + numeroCirculos + numeroTriangulos + numeroTrapecios;
            decimal totalPerimetro = perimetroCuadrados + perimetroCirculos + perimetroTriangulos + perimetroTrapecios;
            decimal totalArea = areaCuadrados + areaCirculos + areaTriangulos + areaTrapecios;

            sb.Append("TOTAL:<br/>");

            switch (idioma)
            {
                case Castellano:
                    sb.AppendFormat("{0} formas Perimetro {1:#.##} Area {2:#.##}", totalFormas, totalPerimetro, totalArea);
                    break;
                case Italiano:
                    sb.AppendFormat("{0} forme Perimetro {1:#.##} Area {2:#.##}", totalFormas, totalPerimetro, totalArea);
                    break;
                case Ingles:
                default:
                    sb.AppendFormat("{0} shapes Perimeter {1:#.##} Area {2:#.##}", totalFormas, totalPerimetro, totalArea);
                    break;
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad <= 0)
                return string.Empty;

            string nombreForma = TraducirForma(tipo, cantidad, idioma);
            string areaTexto = idioma == Castellano ? "Area" : idioma == Italiano ? "Area" : "Area";
            string perimetroTexto = idioma == Castellano ? "Perimetro" : idioma == Italiano ? "Perimetro" : "Perimeter";

            return string.Format("{0} {1} | {2} {3:#.##} | {4} {5:#.##} <br/>",
                cantidad, nombreForma, areaTexto, area, perimetroTexto, perimetro);
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case Cuadrado:
                    if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else if (idioma == Italiano) return cantidad == 1 ? "Quadrato" : "Quadrati";
                    else return cantidad == 1 ? "Square" : "Squares";

                case Circulo:
                    if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else if (idioma == Italiano) return cantidad == 1 ? "Cerchio" : "Cerchi";
                    else return cantidad == 1 ? "Circle" : "Circles";

                case TrianguloEquilatero:
                    if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else if (idioma == Italiano) return cantidad == 1 ? "Triangolo" : "Triangoli";
                    else return cantidad == 1 ? "Triangle" : "Triangles";

                case Trapecio:
                    if (idioma == Castellano) return cantidad == 1 ? "Trapecio" : "Trapecios";
                    else if (idioma == Italiano) return cantidad == 1 ? "Trapezio" : "Trapezi";
                    else return cantidad == 1 ? "Trapezoid" : "Trapezoids";

                default:
                    return string.Empty;
            }
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado:
                    return _lado * _lado;
                case Circulo:
                    return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case TrianguloEquilatero:
                    return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
                case Trapecio:
                    // Área trapecio = ((baseMayor + baseMenor) / 2) * altura
                    return ((_lado + _lado2) / 2) * _altura;
                default:
                    throw new ArgumentOutOfRangeException("Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado:
                    return _lado * 4;
                case Circulo:
                    return (decimal)Math.PI * _lado;
                case TrianguloEquilatero:
                    return _lado * 3;
                case Trapecio:
                    // Asumimos trapecio isósceles para perímetro
                    // Lados no paralelos iguales: calcular usando Pitágoras
                    decimal ladoNoParalelo = (decimal)Math.Sqrt((double)(Math.Pow((double)((_lado - _lado2) / 2), 2) + Math.Pow((double)_altura, 2)));
                    return _lado + _lado2 + 2 * ladoNoParalelo;
                default:
                    throw new ArgumentOutOfRangeException("Forma desconocida");
            }
        }
    }
}
