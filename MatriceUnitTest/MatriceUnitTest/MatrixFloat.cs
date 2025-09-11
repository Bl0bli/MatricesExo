using System;

namespace Maths_Matrices.Tests
{
    public class MatrixFloat
    {
        public int NbLines;
        public int NbColumns;
        public float[,] Matrix;
        
        public float this[int i, int i1]
        {
            get => Matrix[i, i1];
            set => Matrix[i, i1] = value;
        }
        
        #region Constructor / Deconstructor

        public MatrixFloat(float[,] matrix)
        {
            this.Matrix = matrix;
        }
        public MatrixFloat(int m, int n)
        {
            NbLines = m;
            NbColumns = n;
            Matrix = new float[NbLines, NbColumns];
        }
        
        #endregion
        
        public float[,] ToArray2D()
        {
            return Matrix;
        }
        
        public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat m1, MatrixFloat m2)
        {
            int nbColumns = m1.NbColumns + m2.NbColumns;
            MatrixFloat newMatrix = new MatrixFloat(m1.NbLines , nbColumns);

            for (int i = 0; i < m1.NbLines; i++)
            for (int j = 0; j < nbColumns; j++)
            {
                if(j > m1.NbColumns - 1)
                    newMatrix[i, j] = m2[i, 0];
                    
                else newMatrix[i, j] = m1[i, j];
            }

            return newMatrix;
        }
        public (MatrixFloat matrixA, MatrixFloat matrixB) Split(int index)
        {
            int columnIndex = index+ 1;
            if (columnIndex > NbColumns || columnIndex < 0) throw new ArgumentOutOfRangeException();
            
            MatrixFloat matrixA = new MatrixFloat(NbLines, columnIndex);
            MatrixFloat matrixB = new MatrixFloat(NbLines, NbColumns - columnIndex);
            
            for(int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
            {
                if (j < columnIndex)
                {
                    matrixA[i, j] = Matrix[i, j];
                }
                else
                {
                    matrixB[i, j - columnIndex] = Matrix[i, j];
                }
            }
            return (matrixA, matrixB);
        }
    }
}