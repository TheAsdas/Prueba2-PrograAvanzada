using System;

namespace RandomUtils.Exceptions
{
    /// <summary>
    /// Esta excepción se lanza cuando un medidor no está definido dentro del sistema. <br />
    /// Es decir, esta excepción se lanzaría en caso de que el servidor recibiese un tipo de medidor que no sea eléctrico o de tráfico.
    /// </summary>
    public class InvalidMeterException : Exception
    {
        public override string Message => "Este medidor es chanta y está hecho en China.";
    }

    /// <summary>
    /// Esta excepción se lanza cuando la clase de historial no está definida dentro del sistema. <br/> 
    /// Es decir, esta excepción se lanzaría en caso de que el servidor recibiese un tipo de historial que no sea eléctrico o de tráfico.
    /// </summary>
    public class InvalidHistoryException: Exception
    {
        public override string Message => "Esta entrada de historial fue hackeada por los rusos para robarle la elección a José Antonio Kast";
    }
}
