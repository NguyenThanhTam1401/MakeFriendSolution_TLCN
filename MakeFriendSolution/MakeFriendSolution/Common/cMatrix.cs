﻿using System;
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

        private List<double> getVectorLength(double[,] matrix, int row, int column)
        {
            if (row <= 0 || column <= 0)
                return null;

            List<double> vectorLengths = new List<double>();
            for (int i = 0; i < row; i++)
            {
                double sum = 0;
                for (int j = 0; j < column; j++)
                {
                    sum += matrix[i, j] * matrix[i, j];
                }
                var length = Math.Sqrt(sum);
                vectorLengths.Add(length);
            }

            return vectorLengths;
        }

        private List<double> calculateSimilarScore(double[,] matrix, int row, int column)
        {
            if (row <= 0 || column <= 0)
                return null;

            List<double> scores = new List<double>();
            double tichVoHuong;
            double tichDoDai;

            for (int i = 0; i < row; i++)
            {
                tichVoHuong = 0;
                double lengthVectorA = 0;
                double lengthVectorB = 0;

                for (int j = 0; j < column; j++)
                {
                    tichVoHuong += matrix[0, j] * matrix[i, j];

                    lengthVectorA += (matrix[0, j] * matrix[0, j]);
                    lengthVectorB += (matrix[i, j] * matrix[i, j]);
                }

                tichDoDai = Math.Sqrt(lengthVectorA) * Math.Sqrt(lengthVectorB);
                scores.Add(Math.Round(tichVoHuong / tichDoDai * 100, 3));
            }

            return scores;
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

        public List<double> SimilarityCalculate()
        {
            List<double> kq = new List<double>();
            double tichVoHuong;
            double tichDoDai;
            GetLength();
            StandardizedMatrix();

            for (int i = 0; i < this.Row; i++)
            {
                tichVoHuong = 0;

                double lengthVectorA = 0;
                double lengthVectorB = 0;

                for (int j = 0; j < this.Column; j++)
                {
                    tichVoHuong += this.Matrix[0, j] * this.Matrix[i, j];

                    lengthVectorA += (this.Matrix[0, j] * this.Matrix[0, j]);
                    lengthVectorB += (this.Matrix[i, j] * this.Matrix[i, j]);
                }

                tichDoDai = Math.Sqrt(lengthVectorA) * Math.Sqrt(lengthVectorB);

                double result = tichVoHuong / tichDoDai;

                kq.Add(Math.Round(result * 100, 3));
            }
            return kq;
        }

        #endregion
    }
}