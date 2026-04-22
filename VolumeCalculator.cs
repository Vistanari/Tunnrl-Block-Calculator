namespace TunnelBlockCalculator;

/// <summary>
/// Computes the volume of a semi-cylindrical tunnel block that sits on a
/// rectangular base. The solid is shaped like a small road tunnel: the
/// bottom portion is a rectangular prism (the "walls") and the top portion
/// is a half-cylinder whose flat side rests on top of the walls. The axis
/// of the half-cylinder runs along the length of the block, and its
/// diameter equals the width of the rectangular base.
/// </summary>
public static class VolumeCalculator
{
    public static VolumeResult Compute(double length, double width, double wallHeight)
    {
        // Rectangular prism portion: L x W x H
        double rectangularVolume = length * width * wallHeight;

        // Semi-cylindrical roof: half of (pi * r^2 * L) where r = W / 2
        double radius = width / 2.0;
        double semiCylinderVolume = 0.5 * Math.PI * radius * radius * length;

        double totalVolume = rectangularVolume + semiCylinderVolume;

        // Total exterior height of the block (walls + dome)
        double totalHeight = wallHeight + radius;

        return new VolumeResult(
            Length: length,
            Width: width,
            WallHeight: wallHeight,
            Radius: radius,
            TotalHeight: totalHeight,
            RectangularVolume: Math.Round(rectangularVolume, 4),
            SemiCylinderVolume: Math.Round(semiCylinderVolume, 4),
            TotalVolume: Math.Round(totalVolume, 4),
            Formula: "V = (L × W × H) + (½ × π × (W/2)² × L)"
        );
    }
}

public record VolumeResult(
    double Length,
    double Width,
    double WallHeight,
    double Radius,
    double TotalHeight,
    double RectangularVolume,
    double SemiCylinderVolume,
    double TotalVolume,
    string Formula
);
