using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Project
{
    public partial class Form1 : Form
    {
        int NumberOfDisks;
        int start;
        int end;
        int currentDisk;
        int direction;
        int index=0;
        int[] Disks;
        int[] distance;
        List<int> seek_sequence = new List<int>();
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        bool IsaCopy(int x)
        {
            for (int i=0;i<Disks.Length;i++)
            {
                if (x==Disks[i])
                    return false;
            }
            return true;
        }

        public void SSTF()
        {
            seek_sequence.Clear();
            int[] isAccssed = new int[NumberOfDisks];
            int min = 99999;
            int minPos = 0;
            int accessCounter = 0;
            int head = currentDisk;
            int total = 0;
            seek_sequence.Add(currentDisk);
            for (int i = 0; i < isAccssed.Length; i++)
            {
                isAccssed[i] = 1;
            }
            while (accessCounter != NumberOfDisks)
            {
                for (int i = 0; i < Disks.Length; i++)
                {
                    if (Math.Abs(head - Disks[i]) < min && isAccssed[i] == 1)
                    {
                        min = Math.Abs(head - Disks[i]);
                        minPos = i;
                    }
                    else if (Math.Abs(head - Disks[i]) == min)
                    {
                        if (direction == 1 && Disks[i] > Disks[minPos])
                        {
                            minPos = i;
                        }
                    }
                }
                total += min;
                head = Disks[minPos];
                accessCounter++;
                isAccssed[minPos] = 0;
                min = 9999;
                seek_sequence.Add(Disks[minPos]);
            }
            label7.Text = Convert.ToString(total);
            for (int i = 0; i < seek_sequence.Count; i++)
            {
                if (i == 0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }
            }
        }

        public void FCFS()
        {
            seek_sequence.Clear();
            seek_sequence.Add(currentDisk);
            int total = 0;
            int head = currentDisk;
            for (int i = 0; i < NumberOfDisks; i++)
            {
                total += Math.Abs(head - Disks[i]);
                seek_sequence.Add(Disks[i]);

                head = Disks[i];
            }
            for(int i=0;i<seek_sequence.Count;i++)
            {
                if(i ==0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }
            }
            label7.Text = Convert.ToString(total);
        }

        public void LOOK()
        {
            seek_sequence.Clear();
            int head = currentDisk;
            seek_sequence.Add(currentDisk);
            int tot = 0, diff = 0;
            
            int iDisk = -1;
            int obj = Convert.ToInt32(textBox3.Text);
            int iObj = -1;
            int min = 9999999;
            bool end = false, isReach = false;
            List<int> isVisited = new List<int>();
            List<int> notVisited = new List<int>();
            isVisited.Add(head);
            for (int i = 0; i < NumberOfDisks; i++)
            {
                if (min > Math.Abs(obj - Disks[i]))
                {
                    min = Math.Abs(obj - Disks[i]);
                    iObj = i;
                }
                notVisited.Add(Disks[i]);
            }
            while (!end)
            {
                diff = 0;
                min = 99999;
                for (int i = 0; i < notVisited.Count; i++)
                {
                    if (!isReach)
                    {
                        if (head < obj && notVisited[i] > head)
                        {
                            diff = Math.Abs(head - notVisited[i]);
                            if (min > diff)
                            {
                                min = diff;
                                iDisk = i;
                            }
                        }
                        else if (head > obj && notVisited[i] < head)
                        {
                            diff = Math.Abs(head - notVisited[i]);
                            if (min > diff)
                            {
                                min = diff;
                                iDisk = i;
                            }
                        }
                    }
                    else
                    {
                        diff = Math.Abs(head - notVisited[i]);
                        if (min > diff)
                        {
                            min = diff;
                            iDisk = i;
                        }
                    }


                }

                head = notVisited[iDisk];
                if (min != 99999)
                    tot += min;
                seek_sequence.Add(head);
                isVisited.Add(head);
                notVisited.Remove(head);
                if (head == Disks[iObj])
                {
                    isReach = true;
                }
                if (notVisited.Count == 0)
                {
                    end = true;
                    label7.Text = Convert.ToString(tot);
                }
            }

            for (int i = 0; i < seek_sequence.Count; i++)
            {
                if (i == 0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }

            }
        }

        public void CLOOK()
        {
            seek_sequence.Clear();
            int head = currentDisk;
            seek_sequence.Add(currentDisk);
            int tot = 0, diff = 0;
            int iDisk = -1;
            int obj = Convert.ToInt32(textBox3.Text);
            int iObj = -1;
            int min = 9999999;
            bool end = false, isReach = false, isZero = false;
            List<int> isVisited = new List<int>();
            List<int> notVisited = new List<int>();
            isVisited.Add(head);
            for (int i = 0; i < NumberOfDisks; i++)
            {
                if (min > Math.Abs(obj - Disks[i]))
                {
                    min = Math.Abs(obj - Disks[i]);
                    iObj = i;
                }
                notVisited.Add(Disks[i]);
            }
            while (!end)
            {
                diff = 0;
                min = 99999;
                for (int i = 0; i < notVisited.Count; i++)
                {
                    if (!isReach)
                    {
                        if (head < obj && notVisited[i] > head)
                        {
                            diff = Math.Abs(head - notVisited[i]);
                            if (min > diff)
                            {
                                min = diff;
                                iDisk = i;
                            }
                        }
                        else if (head > obj && notVisited[i] < head)
                        {
                            diff = Math.Abs(head - notVisited[i]);
                            if (min > diff)
                            {
                                min = diff;
                                iDisk = i;
                            }
                        }
                    }
                    else
                    {
                        if (head == 0)
                            isZero = true;

                        diff = Math.Abs(head - notVisited[i]);

                        if (min > diff)
                        {
                            min = diff;
                            iDisk = i;
                        }
                    }


                }

                if (isZero)
                {
                    min = Disks[iObj] - notVisited[iDisk];
                    isZero = false;
                }
                head = notVisited[iDisk];
                if (min != 99999)
                {
                    tot += min;
                }
                seek_sequence.Add(head);
                isVisited.Add(head);
                notVisited.Remove(head);
                if (head == Disks[iObj])
                {
                    isReach = true;
                    head = 0;
                }
                if (notVisited.Count == 0)
                {
                    end = true;
                    label7.Text = Convert.ToString(tot);
                }
            }

            for (int i = 0; i < seek_sequence.Count; i++)
            {
                if (i == 0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }

            }
        }

        public void SCAN(int[] arr, int head)
        {
            seek_sequence.Clear();
            int seek_count = 0;
            int distance, cur_track;
            List<int> left = new List<int>(),right = new List<int>();
            if (direction == 0)
                left.Add(0);
            else if (direction == 1)
                right.Add(end);
            seek_sequence.Add(currentDisk);
            for (int i = 0; i < NumberOfDisks; i++)
            {
                if (arr[i] < head)
                    left.Add(arr[i]);
                if (arr[i] > head)
                    right.Add(arr[i]);
            }
            left.Sort();
            right.Sort();
            int breakloop = 0;
            while (true)
            {
                if (direction == 0)
                {
                    for (int i = left.Count - 1; i >= 0; i--)
                    {
                        cur_track = left[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        seek_count += distance;
                        head = cur_track;
                    }
                    direction = 1;
                    breakloop++;
                }
                else if (direction == 1)
                {
                    for (int i = 0; i < right.Count; i++)
                    {
                        cur_track = right[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        seek_count += distance;
                        head = cur_track;
                    }
                    direction = 0;
                    breakloop++;
                }
                else
                    breakloop++;
                if (breakloop >= 2)
                {
                    break;
                }
            }
            label7.Text = Convert.ToString(seek_count);
            for (int i = 0; i < seek_sequence.Count; i++)
            {
                if(i==0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }

            }
        }

        public void CSCAN(int[] arr, int head)
        {
            seek_sequence.Clear();
            int total = 0;
            int distance, cur_track;
            List<int> left = new List<int>(), right = new List<int>();
            left.Add(start);
            right.Add(end);
            seek_sequence.Add(currentDisk);
            for (int i = 0; i < NumberOfDisks; i++)
            {
                if (arr[i] < head)
                    left.Add(arr[i]);
                if (arr[i] > head)
                    right.Add(arr[i]);
            }
            left.Sort();
            right.Sort();
                if (direction == 0)
                {
                    for (int i = left.Count - 1; i >= 0; i--)
                    {
                        cur_track = left[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        total += distance;
                        head = cur_track;
                    }

                    for (int i = right.Count - 1; i >= 0; i--)
                    {
                        cur_track = right[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        total += distance;
                        head = cur_track;
                    }
                }
                else if (direction == 1)
                {
                    for (int i = 0; i < right.Count; i++)
                    {
                        cur_track = right[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        total += distance;
                        head = cur_track;
                    }


                    for (int i = 0; i < left.Count; i++)
                    {
                        cur_track = left[i];
                        seek_sequence.Add(cur_track);
                        distance = Math.Abs(cur_track - head);
                        total += distance;
                        head = cur_track;
                    }
                }
            label7.Text = Convert.ToString(total);
            for (int i = 0; i < seek_sequence.Count; i++)
            {
                if (i == 0)
                {
                    label8.Text = "Sequence: " + Convert.ToString(seek_sequence[i]);
                }
                else
                {
                    label8.Text += " - " + Convert.ToString(seek_sequence[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            distance = new int[NumberOfDisks];
            if (IsDigitsOnly(textBox1.Text))
            {
                NumberOfDisks = Convert.ToInt32(textBox1.Text);
                if(NumberOfDisks>0)
                {
                    Disks = new int[NumberOfDisks];
                    textBox1.Enabled = false;
                    button2.Enabled = true;
                    textBox2.Enabled = true;
                    button1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please enter a bigger number!");
                }
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox2.Text))
            {
                start = Convert.ToInt32(textBox2.Text);
                textBox2.Enabled = false;
                button3.Enabled = true;
                textBox3.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox3.Text))
            {
                end = Convert.ToInt32(textBox3.Text);
                if(start<end)
                {
                    textBox3.Enabled = false;
                    textBox4.Visible = true;
                    label4.Visible = true;
                    button3.Enabled = false;
                    button4.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please enter a bigger number!");
                }
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox4.Text))
            {
                int x = Convert.ToInt32(textBox4.Text);
                if (x >start && x<end && IsaCopy(x))
                {
                    Disks[index] = x;
                    index++;
                    if(index==NumberOfDisks)
                    {
                        button4.Enabled = false;
                        textBox4.Enabled = false;
                        label5.Visible = true;
                        textBox5.Visible = true;
                        button6.Visible = true;
                    }
                    else
                    {
                        button4.Text = Convert.ToString(index + 1);
                        textBox4.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a correct number!");
                }
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex>-1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    FCFS();
                }
                if (comboBox1.SelectedIndex==1)
                {
                    SSTF();
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    SCAN(Disks,currentDisk);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    CSCAN(Disks, currentDisk);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    LOOK();
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    CLOOK();
                }
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                button8.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox5.Text))
            {
                int x = Convert.ToInt32(textBox5.Text);
                if (x > start && x < end && IsaCopy(x))
                {
                    currentDisk = x;
                    textBox5.Enabled = false;
                    button6.Enabled = false;
                    label6.Visible = true;
                    textBox6.Visible = true;
                    button7.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please enter a correct number!");
                }
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox4.Text))
            {
                int x = Convert.ToInt32(textBox6.Text);
                if (x > start && x < end && IsaCopy(x))
                {
                    if(x>currentDisk)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction = 1;
                    }
                    textBox6.Enabled = false;
                    button7.Enabled = false;
                    comboBox1.Visible = true;
                    button5.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please enter a correct number!");
                }
            }
            else
            {
                MessageBox.Show("Please enter numbers!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(NumberOfDisks,start,end,currentDisk,seek_sequence,Disks);
            f2.ShowDialog();
        }
    }
}
