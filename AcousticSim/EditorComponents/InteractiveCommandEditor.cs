using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using Envision;


namespace Envision.Editors
{
   
    public class InteractiveCommandEditor : Envision.Editors.PythonEditor
    {
        private int current_position = 0;
        private int _lastEditablePos = 0;


        public event EventHandler<CommandEnteredEventArgs> CommandEntered;

        public void Init(string initMessage = "")
        {
            this.Text = initMessage + this.Prompt;
            this.updateCursorPosition();
        }
        public String Prompt { get; set; }
        public String SecondaryPrompt { get; set; }
        public int TextLength { get { return this.Text.Length; } }
        public String CurrentWord { get { return _selWord; } }

        public InteractiveCommandEditor()
        {
            this.Prompt = ">> ";
            this.SecondaryPrompt = "...";
            string SyntaxFile = "";
            this.ShowLineNumbers = false;
            if (System.IO.File.Exists(SyntaxFile))
            {
                try
                {
                    this.DescriptionFile = SyntaxFile;
                } catch (Exception ex)
                {
                    Logger.D("SyntaxFile loading failed : " + ex.Message);
                }
            }
            this.KeyPress += AdvancedInteractiveCommandEditor_KeyPress;
            this.KeyDown += AdvancedInteractiveCommandEditor_KeyDown;
            this.RegionChanged += AdvancedInteractiveCommandEditor_RegionChanged;
            updateCursorPosition();
        }

        public new void Paste()
        {
            if (this.SelectionStart >= _lastEditablePos)
            {
                base.Paste();
            }
            else
            {
                 Logger.D("invalid place to paste into");
            }
        }

        void AdvancedInteractiveCommandEditor_RegionChanged(object sender, EventArgs e)
        {
            selectWord();
        }

        void AdvancedInteractiveCommandEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectWord();
            //updateCurrentWord(e);
        }

        void updateCursorPosition()
        {
            current_position = this.Text.Length;
            this.SelectionStart = current_position;
            _lastEditablePos = this.TextLength;
        }


        public new void Clear()
        {
            base.Clear();
            this.insertTermination();
            _lastEditablePos = this.Text.Length;
            this.SelectionStart = _lastEditablePos;

        }


        public void insertTermination(string txt = "", bool appendNewLine = true)
        {

            if (txt != "")
            {
                this.AppendText(txt);
            }
            if (!this.Text.EndsWith("\n") && !(this.Text == "") && appendNewLine)
            {
                this.AppendText("\n");
            }
            this.AppendText(this.Prompt);
        }

        private string _selWord = "";
        private string updateCurrentWord(KeyEventArgs e)
        {
            selectWord();
            if (isModifyingKey(e))
            {
                if (e.KeyCode == Keys.Space)
                {
                    _selWord = "";
                }
                else if (e.KeyCode == Keys.Back)
                {
                    int end = _selWord.Length - 1;
                    if (end >= 0)
                    {
                        _selWord = _selWord.Substring(0, end);
                    }
                    else
                    {
                        _selWord = "";
                    }
                }
            }
            return _selWord;
        }

        private void selectWord()
        {
            int cursorPosition = this.SelectionStart - 1;
            int nextSpace = this.Text.IndexOf(' ', cursorPosition);
            int selectionStart = 0;
            string trimmedString = string.Empty;
            if (nextSpace != -1)
            {
                trimmedString = this.Text.Substring(0, nextSpace);
            }
            else
            {
                trimmedString = this.Text;
            }


            if (trimmedString.LastIndexOf(' ') != -1)
            {
                selectionStart = 1 + trimmedString.LastIndexOf(' ');
                trimmedString = trimmedString.Substring(1 + trimmedString.LastIndexOf(' '));
            }
            _selWord = trimmedString;
        }


        void AdvancedInteractiveCommandEditorEx_KeyDown(object sender, KeyEventArgs e)
        {
            updateCurrentWord(e);
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        void AdvancedInteractiveCommandEditor_KeyDown(object sender, KeyEventArgs e)
        {
            updateCurrentWord(e);
            if (e.KeyCode == Keys.Enter)
            {
                IntelliBox.Visible = false;
                if (_lastEditablePos > this.SelectionStart)
                {
                    this.SelectionStart = this.TextLength;
                }
                string word = this.Text.Substring(_lastEditablePos, this.SelectionStart - _lastEditablePos);
                if (CommandEntered != null)
                {
                    CommandEnteredEventArgs _e = new CommandEnteredEventArgs(word);
                    CommandEntered(this, _e);
                    if (!_e.Handled)
                    {
                        insertTermination();
                    }
                    e.Handled = _e.SuppressEnter;
                    updateCursorPosition();
                }
                else
                {
                    insertTermination("");
                    //e.Handled = true;
                    e.SuppressKeyPress = true;
                    updateCursorPosition();
                }
                _lastEditablePos = this.TextLength;
            }
            else if ((isModifyingKey(e) && (_lastEditablePos > this.SelectionStart)) ||
                (_lastEditablePos == this.SelectionStart && e.KeyCode == Keys.Back))
            {
                if (e.KeyCode == Keys.Back)
                {
                    e.SuppressKeyPress = true; // block keypress
                }
                else
                {
                    updateCursorPosition();
                    AdvancedInteractiveCommandEditor_KeyDown(sender, e);
                }
            }
            //_handleKeyDown(sender, e);
        }



    }

    public class CommandEnteredEventArgs : EventArgs
    {
        public string Command { get; set; }
        public bool Handled { get; set; }
        public bool SuppressEnter { get; set; }
        public CommandEnteredEventArgs(string command)
        {
            Command = command;
            Handled = false;
            SuppressEnter = true;
        }
    }

    public class IsIntelliTriggerEventArgs : EventArgs
    {
        public bool IntelliTrigger { get; set; }
        public KeyEventArgs KeyE { get; set; }
        public IsIntelliTriggerEventArgs(KeyEventArgs e, bool trigger = false)
        {
            this.KeyE = e;
            this.IntelliTrigger = trigger;
        }
    }
    public class IntelliRequestEventArgs : EventArgs
    {
        public List<string> Words = new List<string>();
        private KeyEventArgs _e;
        public string SourceWord = "";
        public KeyEventArgs KeyE
        {
            get { return _e; }
        }
        public IntelliRequestEventArgs(string word, KeyEventArgs KeyE, ref List<string> words)
        {
            this.Words = words;
            _e = KeyE;
            this.SourceWord = word;
        }
    }
    public class IntelliFromatCompletionEventArgs : EventArgs
    {
        public string CompletionString { get; set; }
        public IntelliFromatCompletionEventArgs(string completion)
        {
            CompletionString = completion;
        }
    }
    public class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        private static extern void GetCaretPos(ref System.Drawing.Point pt);

        public static System.Drawing.Point GetCaretPos()
        {
            System.Drawing.Point pt = new System.Drawing.Point();
            GetCaretPos(ref pt);
            return pt;
        }

    }

}
