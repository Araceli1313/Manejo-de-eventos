using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TallerDeEventos.Clases;

namespace TallerDeEventos
{
    public partial class ControlTareas : Form
    {

        List<Tarea> Tareas = new List<Tarea>();

        public ControlTareas()
        {
            InitializeComponent();
            ConfigurarEventosDragDrop(PnlTareasEjecutar);
            ConfigurarEventosDragDrop(PnlTareasProceso);
            ConfigurarEventosDragDrop(PnlTareasFinalizadas);
            
        }


        private void ConfigurarEventosDragDrop(FlowLayoutPanel panel)
        {
            panel.AllowDrop = true;
            panel.DragEnter += new DragEventHandler(Pnl_DragEnter);
            panel.DragDrop += new DragEventHandler(Pnl_DragDrop);
        }


        private void BtnAgregarTareas_Click(object sender, EventArgs e)
        {
            if (TxtIngresarTarea.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la tarea");
                return;
            }


            Tarea nuevatarea = new Tarea(TxtIngresarTarea.Text, "Pendiente");


            Tareas.Add(nuevatarea);


            Label etiquetaTarea = new Label
            {
                Text = nuevatarea.nombre,
                AutoSize = true,
                MinimumSize = new Size(198, 30),
                MaximumSize = new Size(198, 30),
                Padding = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle

            };




            etiquetaTarea.BackColor = Color.RosyBrown;
            etiquetaTarea.Size = new Size(etiquetaTarea.PreferredWidth + 10, etiquetaTarea.Height);



            etiquetaTarea.MouseDown += new MouseEventHandler(etiquetaTarea_MouseDown);


            PnlTareasEjecutar.Controls.Add(etiquetaTarea);


            TxtIngresarTarea.Clear();
        }

        private void BtnEliminarTarea_Click(object sender, EventArgs e)
        {
            if (PnlTareasEjecutar.Controls.Count > 0)
            {
                PnlTareasEjecutar.Controls.RemoveAt(PnlTareasEjecutar.Controls.Count - 1);
            }
            else if (PnlTareasProceso.Controls.Count > 0)
            {
                PnlTareasProceso.Controls.RemoveAt(PnlTareasProceso.Controls.Count - 1);
            }
            else if (PnlTareasFinalizadas.Controls.Count > 0)
            {
                PnlTareasFinalizadas.Controls.RemoveAt(PnlTareasFinalizadas.Controls.Count - 1);
            }
        }
    
        private void TxtIngresarTarea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnAgregarTareas.PerformClick();
                e.SuppressKeyPress = true;
            }
        }


        private void etiquetaTarea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label tarea = sender as Label;
                
                if (tarea != null)
                {
                    tarea.DoDragDrop(tarea, DragDropEffects.Move);
                }
            }
        }


        private void Pnl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        private void Pnl_DragDrop(object sender, DragEventArgs e)
        {
            Label tarea = e.Data.GetData(typeof(Label)) as Label;
            if (tarea != null)
            {
                FlowLayoutPanel targetPanel = sender as FlowLayoutPanel;
                if (targetPanel != null)
                {

                    if (targetPanel == PnlTareasEjecutar)
                    {
                        tarea.BackColor = Color.RosyBrown;
                    }
                    else if (targetPanel == PnlTareasProceso)
                    {
                        tarea.BackColor = Color.Orange;
                    }
                    else if (targetPanel == PnlTareasFinalizadas)
                    {
                        tarea.BackColor = Color.Green;
                    }


                    targetPanel.Controls.Add(tarea);
                    tarea.BringToFront();
                }
            }
        }
    }
}

        
   
