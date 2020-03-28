using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastColoredTextBoxNS;
using System.Drawing;
using System.Windows.Forms;

namespace Envision.Editors
{
    public class PythonEditor : FastColoredTextBoxNS.FastColoredTextBox
    {


        public PythonEditor()
        {
            this.TextChanged += PythonEditor_TextChanged;
            this.KeyDown += PythonEditor_KeyDown;
            this.AutoIndentNeeded += PythonEditor_AutoIndentNeeded;
            this._intelliBox = new ListBox();
        }

        void PythonEditor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            updateCurrentWord(e);
            _handleKeyDown(sender, e);
        }


        public static bool isModifyingKey(KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) ||
                  (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                       (e.Control && e.KeyCode == Keys.V) || (e.Control && e.KeyCode == Keys.X) ||
                       e.KeyCode == Keys.Space || (e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                return true;
            }
            return false;
        }




        void PythonEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            //delete all markers
            this.Range.ClearFoldingMarkers();
            bool inside_error = false;

            var currentIndent = 0;
            var lastNonEmptyLine = 0;
            int i = 0;
            for (i = 0 ; i < this.LinesCount ; i++)
            //while (i < this.LinesCount)
            {
                var line = this[i];
                var spacesCount = line.StartSpacesCount;
                if (spacesCount == line.Count) //empty line
                    continue;
                if (this.Lines[i].StartsWith("Error:["))
                {
                    int _tmp = i;
                    this[_tmp].FoldingStartMarker = "m" + currentIndent;
                    inside_error = true;
                    while (this.Lines[_tmp].Trim() != "]")
                    {
                        _tmp++;
                        if (_tmp >= this.LinesCount) return;
                    }
                    this[_tmp].FoldingEndMarker = "m" + spacesCount;
                    inside_error = false;
                }
                if (!inside_error)
                {

                    if (currentIndent < spacesCount)
                        //append start folding marker
                        this[lastNonEmptyLine].FoldingStartMarker = "m" + currentIndent;
                    else
                        if (currentIndent > spacesCount)
                            //append end folding marker
                            this[lastNonEmptyLine].FoldingEndMarker = "m" + spacesCount;

                    if (this.Lines[i].StartsWith("#region") || this.Lines[i].StartsWith("#endregion"))
                    {
                        if (this.Lines[i].StartsWith("#region"))
                            this[i].FoldingStartMarker = "m" + currentIndent;
                        else
                            this[i].FoldingEndMarker = "m" + spacesCount;
                    }

                }



                currentIndent = spacesCount;
                lastNonEmptyLine = i;
            }
            syntax_highlighter(e);
        }


        Style KeywordsStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        Style FunctionDeclrationStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style FunctionCallStyle = new TextStyle(Brushes.DarkBlue, null, FontStyle.Regular);
        Style StringStyle = new TextStyle(Brushes.DarkGreen, null, FontStyle.Regular);
        Style CommentStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        Style NumberStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Regular);
        Style OperatorStyle = new TextStyle(Brushes.DarkRed, null, FontStyle.Regular);


        void syntax_highlighter(TextChangedEventArgs e)
        {
            //clear styles
            this.Range.ClearStyle(FunctionDeclrationStyle);
            //highlight keywords of LISP
            this.Range.SetStyle(KeywordsStyle, @"\b(and|or|not|for|in|while|def|return|class|import|(\nfrom|^from))\b",
                 System.Text.RegularExpressions.RegexOptions.Multiline);
            // operator styles
            this.Range.SetStyle(OperatorStyle, @"\+|\-|\\|=");
            //number styles
            //this.Range.SetStyle(NumberStyle, @"(\s+|\+|\-|\\|\*|=)-?\d*(\.\d+)?", System.Text.RegularExpressions.RegexOptions.None);
            // highlight string
            this.Range.SetStyle(StringStyle, "(\".*\")|('.*')|(\"\"\".*\"\"\")", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // hightlight comments
            this.Range.SetStyle(CommentStyle, "#.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //find function declarations, highlight all of their entry into the code
            this.Range.SetStyle(FunctionDeclrationStyle, @"\b(def)\s+(?<range>\w+)\(.*\)\:",
            System.Text.RegularExpressions.RegexOptions.None);
            foreach (Range found in this.GetRanges(@"(\(|\s+|\+|\-|\\|\*|=)(?<range>-?\d*(\.\d+)?)\)?"))
            {
                found.SetStyle(NumberStyle);
            }
            foreach (Range found in this.GetRanges(@"\b(def)\s+(?<range>\w+)\(.*\)\:"))
            {
                this.Range.SetStyle(FunctionCallStyle, @"\b" + found.Text + @"\b");
            }

        }

        void PythonEditor_AutoIndentNeeded(object sender, AutoIndentEventArgs e)
        {
            if (e.LineText.EndsWith(":"))
            {
                e.ShiftNextLines = 4;
            }
            else if (e.LineText.Trim() == "")
            {
                e.Shift = -4;
                e.ShiftNextLines = -4;
            }
        }



        private string _selWord = "";
        private char[] word_selectors = { ' ', '(', ')', '.', '=', '\n' };
        private string updateCurrentWord(KeyEventArgs e)
        {
            #region old
            selectWord();
            if (isModifyingKey(e) || e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
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
            Logger.D("selected word :" + _selWord);
            #endregion
            return _selWord;
        }

        private void selectWord()
        {
            try
            {
                int cursorPosition = this.SelectionStart - 1;
                if (cursorPosition >= 0)
                {
                    int nextSpace = this.Text.IndexOf(' ', cursorPosition);
                    int selectionStart = 0;
                    string trimmedString = string.Empty;
                    if (nextSpace != -1)
                    {
                        trimmedString = this.Text.Substring(0, nextSpace);
                    }
                    else
                    {
                        trimmedString = this.Text;//.Substring(cursorPosition);
                    }


                    if (trimmedString.LastIndexOf(' ') != -1)
                    {
                        selectionStart = 1 + trimmedString.LastIndexOf(' ');
                        trimmedString = trimmedString.Substring(1 + trimmedString.LastIndexOf(' '));
                    }
                    _selWord = trimmedString;
                }
            } catch (Exception ex)
            {
                Logger.D(AppGlobals.PyGetTraceback(ex));
            }
        }


        #region "CodeCompletion"
        private ListBox _intelliBox = new ListBox();
        private Form _intelliWindow = new Form();
        private bool intelli_en = true;
        private int trigger_pos = -1;
        private string search_key = "";
        private Keys _intelliKey = Keys.Tab;


        public event EventHandler<IsIntelliTriggerEventArgs> IsIntelliTrigger;
        public event EventHandler<IntelliRequestEventArgs> IntelliRequest;
        public event EventHandler<IntelliFromatCompletionEventArgs> IntelliFromatCompletion;
        private void _posIntelliBox()
        {
            System.Drawing.Point p = NativeMethods.GetCaretPos();
            p = this.Parent.PointToScreen(p);
            this._intelliWindow.Top = p.Y + 25;
            this._intelliWindow.Left = p.X + 5;
        }

        private List<string> intelli_words = new List<string>();
        private void _handleNewIntelliRequest(KeyEventArgs e)
        {
            IsIntelliTriggerEventArgs _trigger = new IsIntelliTriggerEventArgs(e);
            if (IsIntelliTrigger != null)
            {
                IsIntelliTrigger(this, _trigger);
                if (_trigger.IntelliTrigger)
                {
                    e = _trigger.KeyE;
                    if (IntelliRequest != null)
                    {
                        intelli_words.Clear();
                        IntelliRequestEventArgs ex = new IntelliRequestEventArgs(_selWord, e, ref intelli_words);
                        IntelliRequest(this, ex);
                        if (ex.Words.Count > 0)
                        {
                            trigger_pos = this.SelectionStart;
                            search_key = "";
                            _intelliBox.Items.Clear();
                            _intelliBox.Items.AddRange(ex.Words.ToArray());
                            if (_intelliWindow.FormBorderStyle != FormBorderStyle.None || _intelliWindow.IsDisposed)
                            {
                                _intelliWindow = new Form();
                                _intelliWindow.Controls.Add(_intelliBox);
                                _intelliWindow.FormBorderStyle = FormBorderStyle.None;
                                _intelliBox.Dock = DockStyle.Fill;
                                _intelliWindow.Width = 50;
                                _intelliWindow.StartPosition = FormStartPosition.Manual;
                                _intelliWindow.Height = 100;
                                _intelliWindow.ShowInTaskbar = false;
                                _intelliWindow.TopMost = true;
                            }
                            _posIntelliBox();
                            _intelliBox.Show();
                            _intelliWindow.Show();
                            this.Focus();
                        }
                        else
                        {
                            _intelliWindow.Hide();
                            _intelliBox.Hide();
                        }
                    }
                }
            }

        }
        private void _handleKeyDown(object sender, KeyEventArgs e)
        {
            if (!_intelliBox.Visible && e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                this.SelectedText.Insert(0, "    ");
            }
            if (_intelliBox.Visible && e.KeyCode == Keys.Back)
            {
                _intelliBox.Hide();
                _intelliWindow.Hide();
            }
            if (intelli_en)
            {
                if (e.KeyCode == _intelliKey && _intelliBox.Visible)
                {
                    if (_intelliBox.SelectedItem == null && _intelliBox.Items.Count > 0) _intelliBox.SelectedIndex = 0;
                    if (_intelliBox.Items.Count > 0)
                    {
                        string p_string = _intelliBox.SelectedItem.ToString();
                        if (search_key.Length >= 0)
                        {
                            p_string = p_string.Substring(search_key.Length);
                        }
                        if (IntelliFromatCompletion != null)
                        {
                            IntelliFromatCompletionEventArgs _e = new IntelliFromatCompletionEventArgs(p_string);
                            IntelliFromatCompletion(this, _e);
                            p_string = _e.CompletionString;
                        }
                        this.SelectedText = this.SelectedText.Insert(0, p_string);
                        _intelliBox.Hide(); _intelliWindow.Hide();
                        search_key = "";
                        e.SuppressKeyPress = true;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    _intelliBox.Hide(); _intelliWindow.Hide();
                }
                else if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) ||
                            (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                               (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
                {
                    if (_intelliBox.Visible)
                    {
                        string c = ((char)e.KeyValue).ToString().ToLower();
                        if (e.Shift) c = c.ToUpper();
                        search_key = this.Text.Substring(trigger_pos + 1, this.SelectionStart - trigger_pos - 1) + c;
                        _intelliBox.Items.Clear();
                        _intelliBox.Items.AddRange((from string word in intelli_words
                                                    where word.ToString().StartsWith(search_key)
                                                    select word).ToArray());
                        if (_intelliBox.Items.Count > 0) _intelliBox.SelectedIndex = 0;
                        else { _intelliBox.Hide(); _intelliWindow.Hide(); }
                    }
                }
                else if (_intelliBox.Visible && e.KeyCode == Keys.Up)
                {
                    if (_intelliBox.SelectedIndex > 0)
                        _intelliBox.SelectedIndex -= 1;
                    else
                        _intelliBox.SelectedIndex = _intelliBox.Items.Count - 1;
                    e.Handled = true;
                }
                else if (_intelliBox.Visible && e.KeyCode == Keys.Down)
                {
                    if (_intelliBox.SelectedIndex < _intelliBox.Items.Count - 1)
                        _intelliBox.SelectedIndex += 1;
                    else
                        _intelliBox.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if (this.SelectionStart < trigger_pos + 1)
                {
                    _intelliBox.Hide(); _intelliWindow.Hide();
                }
                _handleNewIntelliRequest(e);
            }


        }

        public bool EnableCodeCompletion
        {
            get { return intelli_en; }
            set { intelli_en = value; }
        }

        public ListBox IntelliBox
        {
            get { return _intelliBox; }
            set { this._intelliBox = value; }
        }

        public Keys IntelliCompletionKey
        {
            get { return _intelliKey; }
            set { _intelliKey = value; }
        }

        #endregion
    }
}
