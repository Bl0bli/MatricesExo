using MatriceUnitTest;

namespace Maths_Matrices.Tests
{
    public class MatrixInt
    {
        public int NbLines;
        public int NbColumns;
        public int[,] Matrix; //Tableau a 2 dimensions, équivaut à écrire int[][]

        #region Constructor

        public MatrixInt(int m, int n)
        {
            NbLines = m;
            NbColumns = n;
            Matrix = new int[NbLines, NbColumns];
        }

        public MatrixInt(int[,] matrix)
        {
            NbLines = matrix.GetLength(0);
            NbColumns = matrix.GetLength(1);
            CopyMatrix(matrix);
        }

        public MatrixInt(MatrixInt matrix)
        {
            NbLines = matrix.NbLines;
            NbColumns = matrix.NbColumns;
            CopyMatrix(matrix.Matrix);
        }

        #endregion
        public int this[int i, int i1]
        {
            get => Matrix[i, i1];
            set => Matrix[i, i1] = value;
        }
        
        #region Public methods

        public static MatrixInt Add(MatrixInt matrixA, MatrixInt matrixB)
        {
            MatrixInt newMatrixA = new MatrixInt(matrixA.NbLines, matrixA.NbColumns);
            MatrixInt newMatrixB = new MatrixInt(matrixB.NbLines, matrixB.NbColumns);
            newMatrixA.CopyMatrix(matrixA.Matrix);
            newMatrixB.CopyMatrix(matrixB.Matrix);
            newMatrixA.Add(newMatrixB);
            return newMatrixA;
        }
        public static MatrixInt Identity(int size)
        {
            int[,] matrix = new int[size, size];
            for(int i = 0; i < size; i++)
                matrix[i, i] = 1;

            return new MatrixInt(matrix);
        }
        
        public static MatrixInt Multiply(MatrixInt matrixA, MatrixInt matrixB)
        {
            return matrixA.Multiply(matrixB);
        }

        public static MatrixInt Multiply(MatrixInt matrixB, int factor)
        {
            MatrixInt matrix = new MatrixInt(matrixB.NbLines, matrixB.NbColumns);
            matrix.CopyMatrix(matrixB.Matrix);
            matrix.Multiply(factor);
            return matrix;
        }

        public static MatrixInt Transpose(MatrixInt matrix)
        {
            return matrix.Transpose();
        }

        public MatrixInt Transpose()
        {
            MatrixInt matrix = new MatrixInt(NbColumns, NbLines);
            for(int i = 0; i < NbColumns; i++)
                for(int j = 0; j < NbLines; j++)
                    matrix.Matrix[i,j] = Matrix[j,i];
            
            return matrix;
        }
        public int[,] ToArray2D()
        {
            return Matrix;
        }
        public void Add(MatrixInt matrixB)
        {
            if (NbColumns != matrixB.NbColumns || NbLines != matrixB.NbLines)
            {
                throw new MatrixSumException();
            }
            for(int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                Matrix[i, j] += matrixB.Matrix[i, j];
        }
        public void Multiply(int factor)
        {
            for(int i = 0; i < NbLines; i++)
                for(int j = 0; j < NbColumns; j++)
                    Matrix[i,j] *= factor;
        }

        public MatrixInt Multiply(MatrixInt matrixB)
        {
            if(NbColumns != matrixB.NbLines) throw new MatrixMultiplyException();
            MatrixInt newMatrix = new MatrixInt(NbLines, matrixB.NbColumns);
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < matrixB.NbColumns; j++)
                {
                    int s = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        s += Matrix[i,k] * matrixB.Matrix[k, j];
                    }
                    newMatrix[i,j] = s;
                }
            }
            
            return newMatrix;
        }
        
        public bool IsIdentity()
        {
            return this == Identity(NbLines);
        }
        
        #endregion

        #region operators

        public static MatrixInt operator *(MatrixInt matrixA, MatrixInt matrixB)
        {
            return Multiply(matrixA, matrixB);
        }
        public static MatrixInt operator +(MatrixInt a, MatrixInt b)
        {
            return Add(a, b);
        }

        public static MatrixInt operator -(MatrixInt a, MatrixInt b)
        {
            return Add(a, -b);
        }
        public static MatrixInt operator *(MatrixInt a, int factor)
        {
            return Multiply(a,factor);
        }
        public static MatrixInt operator *(int factor, MatrixInt a)
        {
            return Multiply(a,factor);
        }

        public static MatrixInt operator -(MatrixInt a)
        {
            return Multiply(a, -1);
        }
        public static bool operator ==(MatrixInt a, MatrixInt b)
        {
            if (a.NbLines != a.NbColumns) return false;
            for(int i = 0; i < a.NbLines; i++)
            for(int j = 0; j < a.NbColumns; j++)
                if(a.Matrix[i, j] != b.Matrix[i, j]) return false;
            return true;
        }

        public static bool operator !=(MatrixInt a, MatrixInt b)
        {
            if (a.NbLines != a.NbColumns) return true;
            for(int i = 0; i < a.NbLines; i++)
            for(int j = 0; j < a.NbColumns; j++)
                if(a.Matrix[i, j] != b.Matrix[i, j]) return true;
            return false;
        }

        #endregion
        private void CopyMatrix(int[,] matrix)
        {
            Matrix = new int[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                Matrix[i, j] = matrix[i, j];
        }

    }
}