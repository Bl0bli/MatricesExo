using System;

namespace Maths_Matrices.Tests
{
    public class Vector4
    {
        public float w, x, y, z;

        public Vector4(float x, float y, float z, float w)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector4 Multiply(MatrixFloat matrix)
        {
            float _x = matrix[0, 0] * x + matrix[0, 1] * y + matrix[0, 2] * z + matrix[0, 3] * w;
            float _y = matrix[1, 0] * x + matrix[1, 1] * y + matrix[1, 2] * z + matrix[1, 3] * w;
            float _z = matrix[2, 0] * x + matrix[2, 1] * y + matrix[2, 2] * z + matrix[2, 3] * w;
            float _w = matrix[3, 0] * x + matrix[3, 1] * y + matrix[3, 2] * z + matrix[3, 3] * w;
            
            return new Vector4(_x, _y, _z, _w);
        }
        public static Vector4 Multiply(MatrixFloat matrix, Vector4 v)
        {
            return v.Multiply(matrix);
        }
        
        
        public static Vector4 operator *(MatrixFloat matrix, Vector4 v)
        {
            if ((matrix.NbColumns != matrix.NbLines) || (matrix.NbColumns != 4) || (matrix.NbLines != 4)) throw new ArgumentException();
            return Multiply(matrix,v);
        }
    }
}