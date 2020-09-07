using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Common
{
    public class cMatrix
    {
        //Các thuộc tính, hàm dựng, properties
        #region

        //Thuộc tính
        public int Row { get; set; }

        public int Column { get; set; }
        public double[,] Matrix { get; set; }
        public List<double> ListDoDaiVector { get; set; }

        //Hàm dựng
        public cMatrix()
        {
            this.Row = 0;
            this.Column = 0;
            this.Matrix = new double[1000, 1000];
            this.ListDoDaiVector = new List<double>();
        }

        public cMatrix(int Row, int Column, double[,] Matrix)
        {
            this.Row = Row;
            this.Column = Column;
            this.Matrix = new double[1000, 1000];
            this.Matrix = Matrix;
            this.ListDoDaiVector = new List<double>();
        }

        public cMatrix(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
            this.Matrix = new double[this.Row, this.Column];
            this.ListDoDaiVector = new List<double>();
        }

        #endregion

        //các phương thức nhập xuất
        #region

        public void PrintfMatrix()
        {
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    Console.Write(string.Format("{0}\t", Math.Round(this.Matrix[i, j], 5)));
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void RanDomMatrix(int Row, int Column, int from, int to)
        {
            this.Row = Row;
            this.Column = Column;
            Random rd = new Random();
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this.Matrix[i, j] = rd.Next(from, to);
                }
            }
        }

        #endregion
        //các phương thức tính toán
        #region

        private void GetLength()
        {
            ListDoDaiVector.Clear();
            for (int i = 0; i < this.Row; i++)
            {
                double sum = 0;
                for (int j = 0; j < this.Column; j++)
                {
                    sum += this.Matrix[i, j] * this.Matrix[i, j];
                }
                ListDoDaiVector.Add(Math.Sqrt(sum));
            }
        }

        private void StandardizedMatrix()
        {
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Column; j++)
                {
                    this.Matrix[i, j] = this.Matrix[i, j] / this.ListDoDaiVector[i];
                }
            }
        }

        public List<double> TinhCos()
        {
            List<double> kq = new List<double>();
            double s;
            GetLength();
            StandardizedMatrix();

            int i = 0;
            for (i = 0; i < this.Row; i++)
            {
                s = 0;
                for (int j = 0; j < this.Column; j++)
                {
                    s += this.Matrix[0, j] * this.Matrix[i, j];
                }
                kq.Add(Math.Round(s * 100, 3));
            }
            return kq;
        }

        #endregion
    }
}