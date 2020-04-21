using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDgvMyDist();
            InitializeDgvMyDacash();
        }
        DataGridViewComboBoxColumn dachaName = new DataGridViewComboBoxColumn
        {
            Name = "dachaName",
            HeaderText = @"Адрес дачи",
            DisplayMember = "fullName",
            ValueMember = "id"
        };
        private void InitializeDgvMyDist()
        {
            dgvMyDach.Rows.Clear();
            dgvMyDach.Columns.Clear();
            dgvMyDach.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                Visible = false
            });
            var ownerFIO = new DataGridViewComboBoxColumn
            {
                Name = "ownerName",
                HeaderText = @"ФИО владельца",
                DisplayMember = "fio",
                ValueMember = "id"
            };
            


            dgvMyDach.Columns.AddRange(ownerFIO);
            dgvMyDach.Columns.AddRange(dachaName);
            //dgvMyDach.Columns.Add("area", "Площадь участка");
            //dgvMyDach.Columns.Add("away", "Удаленность от города");
            //dgvMyDach.Columns.Add("name", "Название населенного пунка");

            using (var ctx = new Model())
            {

                ctx.owners.Load();
                ownerFIO.DataSource = ctx.owners.ToList();
                ctx.dacha.Load();
                var districts = ctx.district.ToList();
                var dachas = ctx.dacha.ToList();
                foreach (var dacha in dachas)
                {
                    dacha.district = districts.First(district => district.id == dacha.district_id);
                }
                dachaName.DataSource = dachas;

                using (var ctxx = new Model())
                {
                    foreach (var ow in ctxx.dacha_owners)
                    {
                        var id_dc = ow.id_dacha;
                        foreach (var dh in ctx.dacha)
                        {
                            if (dh.id == id_dc)

                            {
                                var rowIdx = dgvMyDach.Rows.Add(ow.id, ow.id_owners, dh.id);
                                //var dataDict = new Dictionary<string, object>();


                                //dataDict["fio"] = ow.id_owners;
                                //dataDict["name"] = dh.id;

                                //(Dictionary<string, object>, dacha_owners) tp = (dataDict, ow);

                                dgvMyDach.Rows[rowIdx].Tag = ow;


                            }
                        }

                    }
                }
            }

        }

        private void dgvMyDach_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            string nameDacha= " ";
            string Fio= "";
            using (var ctx = new Model())
            {
                
                if (!dgvMyDach.Rows[e.RowIndex].IsNewRow)
                {

                    dgvMyDach[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                    if (dgvMyDach.Rows[e.RowIndex].Tag != null)
                    {
                        var dc = (dacha_owners)dgvMyDach.Rows[e.RowIndex].Tag;
                        foreach (var tmp in ctx.dacha)
                        {
                            if (dc.id_dacha == tmp.id)
                                nameDacha = tmp.name;

                        }

                        foreach (var tmp in ctx.owners)
                        {
                            if (dc.id_owners == tmp.id)
                                Fio = tmp.fio;

                        }
                        if (e.ColumnIndex == 2)
                            dgvMyDach[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                          (nameDacha);
                        else
                            dgvMyDach[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                         (Fio);
                    }
                }
            }
        }

        private void dgvMyDach_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var ctx = new Model())
            {

                // (Dictionary<string, object>, dacha_owners) dc = (Tuple) e.Row.Tag;
                var dc = (dacha_owners)e.Row.Tag;
                ctx.dacha_owners.Attach(dc);
                ctx.dacha_owners.Remove(dc);
                ctx.SaveChanges();
            }
        }

        private void dgvMyDach_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = dgvMyDach.Rows[e.RowIndex];
            bool newRow = row.IsNewRow;
            int rep = 0;
            if (dgvMyDach.IsCurrentRowDirty)
            {
                var cellsWithPotentialErrors = new[] { row.Cells["ownerName"], row.Cells["dachaName"]};
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                    {
                        row.ErrorText = $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым";
                        e.Cancel = true;
                    }
                }
                foreach (DataGridViewRow rw in dgvMyDach.Rows)
                {
                    var dict1 = new dacha_owners
                    {
                        id_dacha = Convert.ToInt32(row.Cells["dachaName"].Value),
                        id_owners = Convert.ToInt32(row.Cells["ownerName"].Value),
                    };
                    var dict2 = (dacha_owners)rw.Tag;   

                    if (row == rw || rw.IsNewRow)
                    {
                        continue;
                    }
                    if (dict1.id_dacha == dict2.id_dacha && dict1.id_owners == dict2.id_owners)
                    {
                        row.ErrorText = $"Значение в строке уже существует";
                        e.Cancel = true;
                        rep = 0;

                    }


                }

                if (!e.Cancel)
                {
                    using (var ctxx = new Model())
                    {
                        var ownId = (int?)row.Cells["id"].Value;
                        if (ownId.HasValue)
                        {
                            var ow = (dacha_owners)row.Tag;
                            ctxx.dacha_owners.Attach(ow);
                            ow.id_dacha = Convert.ToInt32(row.Cells["dachaName"].Value);
                            ow.id_owners = Convert.ToInt32(row.Cells["ownerName"].Value);
                            ctxx.SaveChanges();


                        }
                        else
                        {
                            var ow = new dacha_owners
                            {
                                id_dacha = Convert.ToInt32(row.Cells["dachaName"].Value),
                                id_owners = Convert.ToInt32(row.Cells["ownerName"].Value),
                            };
                            ctxx.dacha_owners.Add(ow);
                            ctxx.SaveChanges();
                            row.Tag = ow;
                        }



                    }

                    row.ErrorText = "";
                    foreach (var cell in cellsWithPotentialErrors)
                    {
                        cell.ErrorText = "";
                    }
                }
            }

        }

        private void dgvMyDach_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvMyDach.IsCurrentRowDirty)
            {
                dgvMyDach.CancelEdit();
                if (dgvMyDach.CurrentRow.Cells["id"].Value != null)
                {
                    var kvp = (dacha_owners)dgvMyDach.CurrentRow.Tag;
                    {
                        dgvMyDach.CurrentRow.Cells[1].Value = kvp.id_owners;
                        dgvMyDach.CurrentRow.Cells[2].Value = kvp.id_dacha;
                        dgvMyDach.CurrentRow.Cells[1].ErrorText = "";
                        dgvMyDach.CurrentRow.Cells[2].ErrorText = "";
                    }
                }
                else
                {
                    dgvMyDach.Rows.Remove(dgvMyDach.CurrentRow);
                }
            }
        }

        private void InitializeDgvMyDacash()
        {
            dgvMyDachas.Rows.Clear();
            dgvMyDachas.Columns.Clear();
            dgvMyDachas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                Visible = false
            });

            dgvMyDachas.Columns.Add("area", "Площадь участка");
            dgvMyDachas.Columns.Add("away", "Удалённость от города");
            dgvMyDachas.Columns.Add("name", "Населённый пункт");
            var distName = new DataGridViewComboBoxColumn
            {
                Name = "distName",
                HeaderText = @"Район",
                DisplayMember = "name",
                ValueMember = "id"
            };


            dgvMyDachas.Columns.AddRange(distName);
            //dgvMyDach.Columns.Add("area", "Площадь участка");
            //dgvMyDach.Columns.Add("away", "Удаленность от города");
            //dgvMyDach.Columns.Add("name", "Название населенного пунка");

            using (var ctx = new Model())
            {

                ctx.district.Load();
                distName.DataSource = ctx.district.Local.ToBindingList();

                foreach (var ow in ctx.dacha)
                {
                    
                    var id_dc = ow.district_id;
                    //using (var ctxx = new OpenModel())
                    //{

                    var rowIdx = dgvMyDachas.Rows.Add(ow.id, ow.area, ow.awayfromtown, ow.name, ow.district_id);
                    //var dataDict = new Dictionary<string, object>();


                    //dataDict["fio"] = ow.id_owners;
                    //dataDict["name"] = dh.id;

                    //(Dictionary<string, object>, dacha_owners) tp = (dataDict, ow);

                    dgvMyDachas.Rows[rowIdx].Tag = ow;





                    //}

                }
            }

        }

        private void dgvMyDachas_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = dgvMyDachas.Rows[e.RowIndex];
            bool newRow = row.IsNewRow;
            int rep = 0;
            bool txt = true;
            if (dgvMyDachas.IsCurrentRowDirty)
            {
                var cellsWithPotentialErrors = new[] { row.Cells["area"], row.Cells["away"], row.Cells["name"], row.Cells["distName"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                    {
                        row.ErrorText = $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым";
                        e.Cancel = true;
                    }
                }

                foreach (var cell in cellsWithPotentialErrors)
                {

                    if (Convert.ToString(cell.Value).Trim().Length > 100)
                    {
                        row.ErrorText = $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть длинее 100 символов";
                        e.Cancel = true;
                    }
                }
                int x;
                if (!Int32.TryParse(Convert.ToString(row.Cells["area"].Value).Trim(), out x))
                {
                    row.ErrorText = $"Значение в столбце '{row.Cells["area"].OwningColumn.HeaderText}' должно быть целым типа int  и не отрицательным";
                    e.Cancel = true;
                    txt = false;
                }
                else
                {
                    if ((Convert.ToInt64(row.Cells["area"].Value) <= 0))
                    {
                        row.ErrorText = $"Значение в столбце '{row.Cells["area"].OwningColumn.HeaderText}' не может быть отрицательным";
                        e.Cancel = true;
                    }
                }

                if (!Int32.TryParse(Convert.ToString(row.Cells["away"].Value).Trim(), out x))
                {
                    row.ErrorText = $"Значение в столбце '{row.Cells["away"].OwningColumn.HeaderText}' должно быть целым типа int  и не отрицательным";
                    e.Cancel = true;
                    txt = false;
                }
                else
                {
                    if ((Convert.ToInt64(row.Cells["away"].Value) <= 0))
                    {
                        row.ErrorText = $"Значение в столбце '{row.Cells["away"].OwningColumn.HeaderText}' не может быть отрицательным";
                        e.Cancel = true;
                    }
                }
                if (txt)
                {
                    foreach (DataGridViewRow rw in dgvMyDachas.Rows)
                    {
                        var dict1 = new dacha
                        {
                            area = Convert.ToInt32(row.Cells["area"].Value),
                            awayfromtown = Convert.ToInt32(row.Cells["away"].Value),
                            name = Convert.ToString(row.Cells["name"].Value).Trim(),
                            district_id = Convert.ToInt32(row.Cells["distName"].Value)
                        };
                        var dict2 = (dacha)rw.Tag;

                        if (row == rw || rw.IsNewRow)
                        {
                            continue;
                        }
                        if (dict1.area == dict2.area && dict1.awayfromtown == dict2.awayfromtown && dict1.name == dict2.name && dict1.district_id == dict2.district_id)
                        {
                            row.ErrorText = $"Значение в строке уже существует";
                            e.Cancel = true;
                            rep = 0;

                        }


                    }
                }
                

                if (!e.Cancel)
                {
                    using (var ctxx = new Model())
                    {
                        var ownId = (int?)row.Cells["id"].Value;
                        if (ownId.HasValue)
                        {
                            var ow = (dacha)row.Tag;
                            ctxx.dacha.Attach(ow);
                            ow.area = Convert.ToInt32(row.Cells["area"].Value);
                            ow.awayfromtown = Convert.ToInt32(row.Cells["away"].Value);
                            ow.district_id = Convert.ToInt32(row.Cells["distName"].Value);
                            ow.name = Convert.ToString(row.Cells["name"].Value).Trim();
                            ctxx.SaveChanges();


                        }
                        else
                        {
                            var ow = new dacha
                            {
                                area = Convert.ToInt32(row.Cells["area"].Value),
                                awayfromtown = Convert.ToInt32(row.Cells["away"].Value),
                                district_id = Convert.ToInt32(row.Cells["distName"].Value),
                                name = Convert.ToString(row.Cells["name"].Value).Trim()
                            };
                            ctxx.dacha.Add(ow);
                            ctxx.SaveChanges();
                            row.Tag = ow;
                        }
                        ctxx.dacha.Load();
                        dachaName.DataSource = ctxx.dacha.ToList();

                    }

                    row.ErrorText = "";
                    foreach (var cell in cellsWithPotentialErrors)
                    {
                        cell.ErrorText = "";
                    }
                }
            }
        }

        private void dgvMyDachas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvMyDachas.IsCurrentRowDirty)
            {
                dgvMyDachas.CancelEdit();
                if (dgvMyDachas.CurrentRow.Cells["id"].Value != null)
                {
                    var kvp = (dacha)dgvMyDachas.CurrentRow.Tag;
                    {
                        dgvMyDachas.CurrentRow.Cells[1].Value = kvp.area;
                        dgvMyDachas.CurrentRow.Cells[2].Value = kvp.awayfromtown;
                        dgvMyDachas.CurrentRow.Cells[3].Value = kvp.name;
                        dgvMyDachas.CurrentRow.Cells[4].Value = kvp.district_id;
                        dgvMyDachas.CurrentRow.Cells[1].ErrorText = "";
                        dgvMyDachas.CurrentRow.Cells[2].ErrorText = "";
                        dgvMyDachas.CurrentRow.Cells[3].ErrorText = "";
                        dgvMyDachas.CurrentRow.Cells[4].ErrorText = "";
                    }
                }
                else
                {
                    dgvMyDachas.Rows.Remove(dgvMyDach.CurrentRow);
                }
            }

        }

        private void dgvMyDachas_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var ctx = new Model())
            {

                // (Dictionary<string, object>, dacha_owners) dc = (Tuple) e.Row.Tag;
                var dc = (dacha)e.Row.Tag;
                ctx.dacha.Attach(dc);
                ctx.dacha.Remove(dc);
                ctx.SaveChanges();
            }
        }

        private void dgvMyDachas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string txt= " ";


            if (!dgvMyDachas.Rows[e.RowIndex].IsNewRow)
            {

                dgvMyDachas[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                if (dgvMyDachas.Rows[e.RowIndex].Tag != null)
                {
                    var dc = (dacha)dgvMyDachas.Rows[e.RowIndex].Tag;
                    switch (e.ColumnIndex)
                    {
                        case 1:
                            txt = Convert.ToString(dc.area);
                            break;
                        case 2:
                            txt = Convert.ToString(dc.awayfromtown);
                            break;
                        case 3:
                            txt = dc.name;
                            break;
                        case 4: 
                            using (var ctx = new Model())
                            {
                                foreach (var dh in ctx.district)
                                {
                                    if (dc.district_id == dh.id)
                                    {
                                        txt = dh.name;
                                        break;
                                    }
                                }
                            }
                                break;


                    }


                    dgvMyDachas[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                      (txt);


                }
            }

        }
    }
}


