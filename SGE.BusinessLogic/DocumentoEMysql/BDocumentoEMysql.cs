using SGE.DataAccess;
using SGE.Entity;
namespace SGE.BusinessLogic
{
    public class BDocumentoEMysql
    {
        DocumentoEMysqlData docData = new DocumentoEMysqlData();
        public bool InsertarDocumento(EDocumentoElectronicoMysql doc)
        {
            return docData.InsertarDocumento(doc);
        }
    }
}
