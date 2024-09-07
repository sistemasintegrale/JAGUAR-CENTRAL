using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace SGE.WindowForms.Modules
{
    public class BSControls
    {
        public static void LoaderLook(LookUpEdit Control, object DataSource, string FieldDisplay, string FieldValue, bool DefaulValue)
        {
            var _with1 = Control;
            _with1.Properties.DataSource = DataSource;
            _with1.Properties.Columns.Clear();
            _with1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(FieldDisplay, 200, "Descripción"));
            _with1.Properties.DisplayMember = FieldDisplay;
            _with1.Properties.ValueMember = FieldValue;
            if (DefaulValue)
                _with1.ItemIndex = 0;
            else
                _with1.EditValue = null;

        }
        public static void LoaderLookRepository(RepositoryItemLookUpEdit Control, object DataSource, string FieldDisplay, string FieldValue, bool DefaulValue)
        {
            var _with1 = Control;
            Control.DataSource = DataSource;
            Control.Columns.Clear();
            Control.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(FieldDisplay, 200, "Descripción"));
            Control.DisplayMember = FieldDisplay;
            Control.ValueMember = FieldValue;

        }

    }
}
