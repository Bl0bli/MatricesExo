namespace Maths_Matrices.Tests
{
    public static class MatrixRowReductionAlgorithm
    {
        public static (MatrixFloat m1, MatrixFloat m2) Apply(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(m1, m2);

            for (int i= 0, j =0; i < augmentedMatrix.NbLines; i++, j++)
            {
                int indexInColum = j;
                float valueInColum = augmentedMatrix[i, j];
                
                for (int k = i; k < i; k++)
                {
                    if (augmentedMatrix[k, j] > valueInColum)
                    {
                        indexInColum = k;
                        valueInColum = augmentedMatrix[k, j];
                    }
                }
                
                if (valueInColum != 0)
                {
                    if(indexInColum != i) MatrixElementaryOperations.SwapLines(augmentedMatrix, i, indexInColum);
                    MatrixElementaryOperations.MultiplyLine(augmentedMatrix, i, 1/augmentedMatrix[i,j]);

                    for (int r = 0; r < augmentedMatrix.NbLines; r++)
                    {
                        if(i != indexInColum) MatrixElementaryOperations.AddLineToAnother(augmentedMatrix, i, r, -augmentedMatrix[r, j]);
                    }
                }
            }

            return augmentedMatrix.Split(augmentedMatrix.NbColumns - 1);
        }
    }
}