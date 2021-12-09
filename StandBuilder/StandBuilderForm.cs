using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace StandBuilder
{
    public partial class StandBuilderForm : Form
    {
        private TextWriter Writer;
        private Dictionary<string, string> ArgsList { get; }
        private VSphereManager VsManager { get; }

        public StandBuilderForm()
        {
            InitializeComponent();
            ArgsList = new Dictionary<string, string>();
            VsManager = new VSphereManager();
        }

        private void SelectDiagramFile()
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = @"LanFlow Model (*.edg)|*.edg|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (!Equals(openFileDialog1.ShowDialog(), DialogResult.OK)) return;
            try
            {
                Console.WriteLine($@"Opening the {openFileDialog1.FileName}...");

                VsManager.InitModel(openFileDialog1.FileName);

                Console.WriteLine(@"Successfully opened");
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void Start()
        {
            ConsoleOutputTextBox.Clear();

            ServerTextBox.Clear();
            UsernameTextBox.Clear();
            PasswordTextBox.Clear();
            ScriptAutoCloseCheckBox.Checked = true;
            DatacenterComboBox.ResetText();
            DatacenterComboBox.Items.Clear();
            DatastoreComboBox.ResetText();
            DatastoreComboBox.Items.Clear();
            HostComboBox.ResetText();
            HostComboBox.Items.Clear();
            GroupFolderNameTextBox.Clear();
            StandNameTextBox.Clear();
            SnapshotNameTextBox.Clear();
            VDSwitchNameTextBox.Clear();

            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = false;
            tableLayoutPanel3.Visible = false;
            tableLayoutPanel4.Visible = false;

            Console.WriteLine(@"Waiting for connection parameters...");
        }

        private void StandBuilderForm_Load(object sender, EventArgs e)
        {
            Writer = new TextBoxStreamWriter(ConsoleOutputTextBox);

            Console.SetOut(Writer);

            Start();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ArgsList.Add("Server", ServerTextBox.Text);
            ArgsList.Add("Username", UsernameTextBox.Text);
            ArgsList.Add("Password", PasswordTextBox.Text);
            ArgsList.Add("AutoClose", ScriptAutoCloseCheckBox.Checked.ToString());

            tableLayoutPanel1.Visible = false;

            Console.WriteLine(@"Waiting for diagram file selection...");

            SelectDiagramFile();

            var datacenters = new List<string>();
            var datastores = new List<string>();
            var hosts = new List<string>();

            Console.WriteLine(@"Waiting for initialization...");

            VsManager.InitProperties(ArgsList, datacenters, datastores, hosts);

            DatacenterComboBox.Items.AddRange(datacenters.ToArray());
            DatastoreComboBox.Items.AddRange(datastores.ToArray());
            HostComboBox.Items.AddRange(hosts.ToArray());

            tableLayoutPanel2.Visible = true;

            Console.WriteLine(@"Waiting for datacenter, datastore and host selection...");
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            ArgsList.Add("Datacenter", DatacenterComboBox.Text);
            ArgsList.Add("Datastore", DatastoreComboBox.Text);
            ArgsList.Add("Host", HostComboBox.Text);

            tableLayoutPanel2.Visible = false;

            tableLayoutPanel3.Visible = true;

            Console.WriteLine(@"Waiting for names selection...");
        }

        private void BuildStandButton_Click(object sender, EventArgs e)
        {
            ArgsList.Add("TemplatesFolder", MachineTemplatesFolderNameTextBox.Text);
            ArgsList.Add("GroupFolder", GroupFolderNameTextBox.Text);
            ArgsList.Add("Stand", StandNameTextBox.Text);
            ArgsList.Add("Snapshot", SnapshotNameTextBox.Text);
            ArgsList.Add("VDSwitch", VDSwitchNameTextBox.Text);

            tableLayoutPanel3.Visible = false;

            Console.WriteLine(@"Building stand...");

            VsManager.BuildStand(ArgsList);

            Console.WriteLine(@"Done!");

            tableLayoutPanel4.Visible = true;
        }

        private void BuildAnotherButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}