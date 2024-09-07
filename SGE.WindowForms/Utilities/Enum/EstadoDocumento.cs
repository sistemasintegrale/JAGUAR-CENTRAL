namespace SGE.WindowForms
{
    public enum EstadoDocumento : int
    {
        #region estado envio

        bajasporEnviar = 1,
        bajasEnviadas = 2,
        PendienteObservadoEnvio = 3,

        #endregion estado envio

        #region estado de sunat

        //pendientesPorEnviar = 4,
        enviadoSunat = 1,
        aprobado = 2,
        rechazado = 3,
        pendienteValidar = 4,
        //excepcionSunat = 4,


        aprobadoResumen = 1,
        rechazadoResumen = 2,

        #endregion estado de sunat

        rangoRechazadoMin = 2000,
        rangoRechazadoMax = 3999,
        rangoObservadoMax = 4000,
        rangoExcepcionMin = 100,
        rangoExcepcionMax = 1999
    }
}