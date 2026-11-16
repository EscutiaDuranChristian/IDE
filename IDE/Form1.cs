using Compilador_Java.Compilacion;
using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Sintactico.GCL;
namespace IDE
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            dataGridView1.DataSource = null;
            listView2.Items.Clear();

            var res = Compilar.CompilarCodigo(textBox1.Text);
            if (res.Item2 != null) dataGridView1.DataSource = res.Item2;
            if (res.Item3 != null)
            {
                foreach (var s in res.Item3)
                    listView2.Items.Add(s.ToString());
            }

            var exceptions = CompilerException.GetExceptions();
            if (exceptions.Count > 0)
                textBox2.Text = string.Join(Environment.NewLine,
                    exceptions.Select(s => s.Item1 + s.Item2));

            CompilerException.Clear();
            RuleBase.Clear();
            SystemMethods.Restart();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listView2.View = View.List;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();

            dialog.Title = "Selecciona un archivo Java";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string codigo = File.ReadAllText(dialog.FileName);
                textBox1.Text = codigo;
            }
        }
    }
}
