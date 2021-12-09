using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StandBuilder
{
    public class TextBoxStreamWriter : TextWriter
    {
        private readonly TextBox Output;

        public TextBoxStreamWriter(TextBox output)
        {
            Output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);

            Output.AppendText(value.ToString());
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}