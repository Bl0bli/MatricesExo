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
            this.NbLines = matrix.GetLength(0);
            this.NbColumns = matrix.GetLength(1);
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
                    newMatrix[i, j] = m2[i, j - m1.NbColumns];
                    
                else newMatrix[i, j] = m1[i, j];
            }

            return newMatrix;
        }
        
        public void Add(MatrixFloat matrixB)
        {
            if (NbColumns != matrixB.NbColumns || NbLines != matrixB.NbLines)
            {
                throw new MatrixSumException();
            }
            for(int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                Matrix[i, j] += matrixB.Matrix[i, j];
        }
        public (MatrixFloat matrixA, MatrixFloat matrixB) Split(int index)
        {
            int columnIndex = index;
            if (columnIndex >= NbColumns || columnIndex < 0) throw new ArgumentOutOfRangeException();
            
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

        public static float Determinant(MatrixFloat matrix)
        {
            int sign = 1;
            float determinant = 0;
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                MatrixFloat m = new MatrixFloat(matrix.Matrix);
                int j = i;
                while (m.NbColumns != 2)
                {
                    m = m.SubMatrix(0, j);
                    j++;
                }
                determinant += sign * matrix.Matrix[0, i] * ((m.Matrix[0, 0] * m.Matrix[1,1]) - (m.Matrix[1, 0] * m.Matrix[0, 1]));
                sign = -sign;

                if (matrix.NbColumns == 2) break;
            }
            return determinant;
        }

        public MatrixFloat SubMatrix(int lineIndex, int columnIndex)
        {
            MatrixFloat matrixA = new MatrixFloat(NbLines - 1, NbColumns - 1);
            for (int i = 0, k = 0; i < NbLines; i++)
            {
                if (i == lineIndex) continue;

                for (int j = 0, l = 0; j < NbColumns; j++)
                {
                    if (j == columnIndex) continue;
                    matrixA[k, l] = Matrix[i, j];
                    l++;
                }

                k++;
            }
            return matrixA;
        }

        public static MatrixFloat SubMatrix(MatrixFloat m, int lineIndex, int columnIndex)
        {
            return m.SubMatrix(lineIndex, columnIndex);
        }

        public static MatrixFloat Identity(int size)
        {
            float[,] matrix = new float[size, size];
            for(int i = 0; i < size; i++)
                matrix[i, i] = 1;

            return new MatrixFloat(matrix);
        }
        public static MatrixFloat InvertByRowReduction(MatrixFloat m)
        {
            return m.InvertByRowReduction();
        }
        
        public MatrixFloat InvertByRowReduction()
        {
            (MatrixFloat matrixA, MatrixFloat identity) = MatrixRowReductionAlgorithm.Apply(this, Identity(NbColumns));
            if(matrixA != Identity(NbColumns)) throw new MatrixInvertException();
            return identity;
        }
        
        public static MatrixFloat Multiply(MatrixFloat matrixA, MatrixFloat matrixB)
        {
            return matrixA.Multiply(matrixB);
        }

        public static MatrixFloat Multiply(MatrixFloat matrixB, float factor)
        {
            MatrixFloat matrix = new MatrixFloat(matrixB.NbLines, matrixB.NbColumns);
            matrix.CopyMatrix(matrixB.Matrix);
            matrix.Multiply(factor);
            return matrix;
        }
        
        public void Multiply(float factor)
        {
            for(int i = 0; i < NbLines; i++)
            for(int j = 0; j < NbColumns; j++)
                Matrix[i,j] *= factor;
        }

        public MatrixFloat Multiply(MatrixFloat matrixB)
        {
            if(NbColumns != matrixB.NbLines) throw new MatrixMultiplyException();
            MatrixFloat newMatrix = new MatrixFloat(NbLines, matrixB.NbColumns);
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < matrixB.NbColumns; j++)
                {
                    float s = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        s += Matrix[i,k] * matrixB.Matrix[k, j];
                    }
                    newMatrix[i,j] = s;
                }
            }
            
            return newMatrix;
        }
        public static MatrixFloat Add(MatrixFloat matrixA, MatrixFloat matrixB)
        {
            MatrixFloat newMatrixA = new MatrixFloat(matrixA.NbLines, matrixA.NbColumns);
            MatrixFloat newMatrixB = new MatrixFloat(matrixB.NbLines, matrixB.NbColumns);
            newMatrixA.CopyMatrix(matrixA.Matrix);
            newMatrixB.CopyMatrix(matrixB.Matrix);
            newMatrixA.Add(newMatrixB);
            return newMatrixA;
        }
        private void CopyMatrix(float[,] matrix)
        {
            Matrix = new float[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                Matrix[i, j] = matrix[i, j];
        }
        
        #region operators

        public static MatrixFloat operator *(MatrixFloat matrixA, MatrixFloat matrixB)
        {
            return Multiply(matrixA, matrixB);
        }
        public static MatrixFloat operator +(MatrixFloat a, MatrixFloat b)
        {
            return Add(a, b);
        }

        public static MatrixFloat operator -(MatrixFloat a, MatrixFloat b)
        {
            return Add(a, -b);
        }
        public static MatrixFloat operator *(MatrixFloat a, int factor)
        {
            return Multiply(a,factor);
        }
        public static MatrixFloat operator *(int factor, MatrixFloat a)
        {
            return Multiply(a,factor);
        }

        public static MatrixFloat operator -(MatrixFloat a)
        {
            return Multiply(a, -1);
        }
        public static bool operator ==(MatrixFloat a, MatrixFloat b)
        {
            if (a.NbLines != a.NbColumns) return false;
            for(int i = 0; i < a.NbLines; i++)
            for(int j = 0; j < a.NbColumns; j++)
                if(a.Matrix[i, j] != b.Matrix[i, j]) return false;
            return true;
        }

        public static bool operator !=(MatrixFloat a, MatrixFloat b)
        {
            if (a.NbLines != a.NbColumns) return true;
            for(int i = 0; i < a.NbLines; i++)
            for(int j = 0; j < a.NbColumns; j++)
                if(a.Matrix[i, j] != b.Matrix[i, j]) return true;
            return false;
        }

        #endregion
    }
}