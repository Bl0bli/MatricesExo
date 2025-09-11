namespace Maths_Matrices.Tests
{
    public static class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt m, int indexA, int indexB)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                int tempValue = m[indexA, i];
                m[indexA, i] = m[indexB, i];
                m[indexB, i] = tempValue;
            }
        }
        public static void SwapLines(MatrixFloat m, int indexA, int indexB)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                float tempValue = m[indexA, i];
                m[indexA, i] = m[indexB, i];
                m[indexB, i] = tempValue;
            }
        }

        public static void SwapColumns(MatrixInt m, int indexA, int indexB)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                int tempValue = m[i, indexA];
                m[i, indexA] = m[i, indexB];
                m[i, indexB] = tempValue;
            }
        }
        
        public static void SwapColumns(MatrixFloat m, int indexA, int indexB)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                float tempValue = m[i, indexA];
                m[i, indexA] = m[i, indexB];
                m[i, indexB] = tempValue;
            }
        }

        public static void MultiplyLine(MatrixInt m, int index, int factor)
        {
            if (factor == 0) throw new MatrixScalarZeroException();
            for (int i = 0; i < m.NbColumns; i++)
            {
                m[index, i] *= factor;
            }
        }
        
        public static void MultiplyLine(MatrixFloat m, int index, float factor)
        {
            if (factor == 0) throw new MatrixScalarZeroException();
            for (int i = 0; i < m.NbColumns; i++)
            {
                m[index, i] *= factor;
            }
        }

        public static void MultiplyColumn(MatrixInt m, int index, int factor)
        {
            if (factor == 0) throw new MatrixScalarZeroException();
            for (int i = 0; i < m.NbLines; i++)
            {
                m[i, index] *= factor;
            }
        }

        public static void AddLineToAnother(MatrixInt m, int lineToAdd, int lineTarget, int coeff)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                m[lineTarget, i] += m[lineToAdd, i] * coeff;
            }
        }
        public static void AddLineToAnother(MatrixFloat m, int lineToAdd, int lineTarget, float coeff)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                m[lineTarget, i] += m[lineToAdd, i] * coeff;
            }
        }

        public static void AddColumnToAnother(MatrixInt m, int columnToAdd, int columdTarget, int coeff)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                m[i, columdTarget] += m[i, columnToAdd] * coeff;
            }
        }
    }
}