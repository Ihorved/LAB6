using System;

public class Quaternion
{
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Quaternion(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    public static Quaternion operator +(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(q1.W + q2.W, q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z);
    }

    public static Quaternion operator -(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(q1.W - q2.W, q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z);
    }

    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        double w = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
        double x = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
        double y = q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X;
        double z = q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W;

        return new Quaternion(w, x, y, z);
    }

    public double Norm()
    {
        return Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
    }

    public Quaternion Conjugate()
    {
        return new Quaternion(W, -X, -Y, -Z);
    }

    public Quaternion Inverse()
    {
        double normSquared = W * W + X * X + Y * Y + Z * Z;
        if (normSquared == 0)
        {
            throw new InvalidOperationException("Cannot invert a quaternion with zero norm.");
        }

        double invNormSquared = 1.0 / normSquared;
        Quaternion conjugate = Conjugate();
        return new Quaternion(conjugate.W * invNormSquared, conjugate.X * invNormSquared, conjugate.Y * invNormSquared, conjugate.Z * invNormSquared);
    }

    public static bool operator ==(Quaternion q1, Quaternion q2)
    {
        return q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y && q1.Z == q2.Z;
    }

    public static bool operator !=(Quaternion q1, Quaternion q2)
    {
        return !(q1 == q2);
    }

    public double[,] ToRotationMatrix()
    {
        double[,] rotationMatrix = new double[3, 3];

        rotationMatrix[0, 0] = 1 - 2 * (Y * Y + Z * Z);
        rotationMatrix[0, 1] = 2 * (X * Y - W * Z);
        rotationMatrix[0, 2] = 2 * (X * Z + W * Y);

        rotationMatrix[1, 0] = 2 * (X * Y + W * Z);
        rotationMatrix[1, 1] = 1 - 2 * (X * X + Z * Z);
        rotationMatrix[1, 2] = 2 * (Y * Z - W * X);

        rotationMatrix[2, 0] = 2 * (X * Z - W * Y);
        rotationMatrix[2, 1] = 2 * (Y * Z + W * X);
        rotationMatrix[2, 2] = 1 - 2 * (X * X + Y * Y);

        return rotationMatrix;
    }

    public static Quaternion FromRotationMatrix(double[,] rotationMatrix)
    {
        if (rotationMatrix.GetLength(0) != 3 || rotationMatrix.GetLength(1) != 3)
        {
            throw new ArgumentException("Rotation matrix should be a 3x3 matrix.");
        }

        double trace = rotationMatrix[0, 0] + rotationMatrix[1, 1] + rotationMatrix[2, 2];

        if (trace > 0)
        {
            double s = 0.5 / Math.Sqrt(trace + 1);
            double w = 0.25 / s;
            double x = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) * s;
            double y = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) * s;
            double z = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) * s;

            return new Quaternion(w, x, y, z);
        }
        else
        {
            if (rotationMatrix[0, 0] > rotationMatrix[1, 1] && rotationMatrix[0, 0] > rotationMatrix[2, 2])
            {
                double s = 2 * Math.Sqrt(1 + rotationMatrix[0, 0] - rotationMatrix[1, 1] - rotationMatrix[2, 2]);
                double w = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) / s;
                double x = 0.25 * s;
                double y = (rotationMatrix[0, 1] + rotationMatrix[1, 0]) / s;
                double z = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;

                return new Quaternion(w, x, y, z);
            }
            else if (rotationMatrix[1, 1] > rotationMatrix[2, 2])
            {
                double s = 2 * Math.Sqrt(1 + rotationMatrix[1, 1] - rotationMatrix[0, 0] - rotationMatrix[2, 2]);
                double w = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) / s;
                double x = (rotationMatrix[0, 1] + rotationMatrix[1, 0]) / s;
                double y = 0.25 * s;
                double z = (rotationMatrix[1, 2] + rotationMatrix[2, 1]) / s;

                return new Quaternion(w, x, y, z);
            }
            else
            {
                double s = 2 * Math.Sqrt(1 + rotationMatrix[2, 2] - rotationMatrix[0, 0] - rotationMatrix[1, 1]);
                double w = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) / s;
                double x = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;
                double y = (rotationMatrix[1, 2] + rotationMatrix[2, 1]) / s;
                double z = 0.25 * s;

                return new Quaternion(w, x, y, z);
            }
        }
    }
}
