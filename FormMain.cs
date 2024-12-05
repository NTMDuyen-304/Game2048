using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game2048
{
    public partial class FormMain : Form
    {
        private int tag = 5; // khoang cach giua cac o
        private Random rand = new Random();
        private int score = 0;
        private Label[,] cardLabel = new Label[4,4];
        int[,] cards = new int[4, 4];
        public FormMain()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init label
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cardLabel[i, j] = new Label();
                    cardLabel[i, j].Location = new Point(tag + i * (100 + tag), tag + j * (100 + tag));
                    cardLabel[i, j].Size = new Size(100, 100);
                    cardLabel[i, j].TabIndex = i * 4 + j;
                    cardLabel[i, j].Name = String.Format("lb%d%d", i, j);
                    cardLabel[i, j].BackColor = Color.FromName("ActiveBorder");
                    cardLabel[i, j].Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    cardLabel[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(cardLabel[i, j]);
                }
            }

            // su kien game
            InitCards();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // dien gia tri ra cac label
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (cards[i, j] == 0)
                        cardLabel[i, j].Text = "";
                    else
                        cardLabel[i, j].Text = cards[i, j].ToString();
                    SetCardColor(i, j);
                }
            }
            lbScore.Text = score.ToString();
        }
        /// <summary>
        /// tao card ngau nhien
        /// </summary>
        bool CreateRandomCard()
        {
            bool isDo = false;
            List<int> test = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                if (cards[i / 4, i % 4] == 0)
                {
                    test.Add(i);
                    isDo = true;
                }
            }
            if (test.Count > 0)
            {
                int set = test[rand.Next(0, test.Count - 1)];
                while (cards[set / 4, set % 4] != 0 && test.Count > 1)
                {
                    test.Remove(set);
                    set = test[rand.Next(0, test.Count - 1)];
                }
                cards[set / 4, set % 4] = rand.Next(1, 100) > 90 ? 4 : 2;
                score += cards[set / 4, set % 4];
            }
            return isDo;
        }
        /// <summary>
        /// doUp, doDown, doRight,doLeft
        /// </summary>
        /// <returns></returns>
        #region Su kien Move
        bool DoUp()
        {
            bool isDo = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int y1 = y + 1; y1 < 4; y1++)
                    {
                        if (cards[x, y1] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x, y1];
                                cards[x, y1] = 0;
                                y--;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x, y1])
                            {
                                cards[x, y] *= 2;
                                cards[x, y1] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if(isDo)
                CreateRandomCard();
            return isDo;
        }
        
        bool DoDown()
        {
            bool isDo = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y >= 1; y--)
                {
                    for (int y1 = y - 1; y1 >= 0; y1--)
                    {
                        if (cards[x, y1] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x, y1];
                                cards[x, y1] = 0;
                                y++;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x, y1])
                            {
                                cards[x, y] *= 2;
                                cards[x, y1] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                CreateRandomCard();
            return isDo;
        }

        bool DoRight()
        {
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 3; x >= 1; x--)
                {
                    for (int x1 = x - 1; x1 >= 0; x1--)
                    {
                        if (cards[x1, y] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x1, y];
                                cards[x1, y] = 0;
                                x++;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x1, y])
                            {
                                cards[x, y] *= 2;
                                cards[x1, y] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                CreateRandomCard();
            return isDo;
        }

        bool DoLeft()
        {
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int x1 = x + 1; x1 < 4; x1++)
                    {
                        if (cards[x1, y] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x1, y];
                                cards[x1, y] = 0;
                                x--;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x1, y])
                            {
                                cards[x, y] *= 2;
                                cards[x1, y] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                CreateRandomCard();
            return isDo;
        }
        #endregion


        /// <summary>
        /// dung phim dieu huong tren ban phim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool moved = false;
         
            switch (e.KeyData)
            {
                case Keys.Up:
                    moved = DoUp();
                    break;
                case Keys.Down:
                    moved = DoDown();
                    break;
                case Keys.Left:
                    moved = DoLeft();
                    break;
                case Keys.Right:
                    moved = DoRight();
                    break;
            }
            if (moved)
            {
                this.Refresh();

                // Kiểm tra game over sau mỗi lần di chuyển
                if (CheckGameOver())
                {
                    DialogResult dia = MessageBox.Show(
                        "Điểm của bạn: " + score.ToString() + "\n" +
                        "Bạn có muốn chơi lại không?",
                        "GAME2048",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (dia == DialogResult.No)
                        Application.Exit();
                    else
                        InitCards();
                }
            }

        }

        bool CheckGameOver()
        {

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if ( cards[x, y] == 0 || 
                        (y < 3 && cards[x,y] == cards[x,y+1]) || 
                        (x < 3 && cards[x, y] == cards[x + 1, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void InitCards()
        {
            score = 0;
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    cards[x, y] = 0;
            CreateRandomCard();
            CreateRandomCard();
            this.Refresh();
        }
        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            // Khởi tạo lại game mới
            InitCards();
            this.Focus();
            this.Select();
        }
        
        void SetCardColor(int x, int y)
        {
            switch (cards[x, y])
            {
                case 0: cardLabel[x, y].BackColor = Color.FromArgb(220,220,220); break;
                case 2: cardLabel[x, y].BackColor = Color.FromArgb(217, 237, 146); break;
                case 4: cardLabel[x, y].BackColor = Color.FromArgb(181, 228, 140); break;
                case 8: cardLabel[x, y].BackColor = Color.FromArgb(153, 217, 140); break;
                case 16: cardLabel[x, y].BackColor = Color.FromArgb(118, 200, 147); break;
                case 32: cardLabel[x, y].BackColor = Color.FromArgb(82, 182, 154); break;
                case 64: cardLabel[x, y].BackColor = Color.FromArgb(52, 160, 164); break;
                case 128: cardLabel[x, y].BackColor = Color.FromArgb(22, 138, 173); break;
                case 256: cardLabel[x, y].BackColor = Color.FromArgb(26, 117, 159); break;
                case 512: cardLabel[x, y].BackColor = Color.FromArgb(30, 96, 145); break;
                case 1024: cardLabel[x, y].BackColor = Color.FromArgb(24, 78, 119); break;
                case 2048: cardLabel[x, y].BackColor = Color.FromArgb(192, 192, 255); break;
                case 4096: cardLabel[x, y].BackColor = Color.FromArgb(128, 128, 255); break;
                case 8192: cardLabel[x, y].BackColor = Color.FromArgb(255, 192, 255); break;
            }
        }

    }
}
