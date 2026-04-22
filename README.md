# Tunnel Block Volume Calculator

A modern ASP.NET Core (.NET 10) web application that calculates the volume of a
**semi-cylindrical tunnel block with a rectangular base** — an irregular solid
shaped like a small road tunnel. The bottom is a rectangular prism (the
"walls") and the top is a half-cylinder (the "dome") whose diameter equals the
width of the base.

The frontend uses **Three.js** to render the block as an interactive 3D object,
and the backend exposes a simple JSON API for the volume calculation.

## Formula

```
V = (L × W × H) + (½ × π × (W / 2)² × L)
```

Where:

- `L` = length of the block
- `W` = width of the rectangular base (also = diameter of the semi-cylinder)
- `H` = height of the rectangular wall portion
- `r` = `W / 2` (radius of the semi-cylinder)

## Project Structure

```
TunnelBlockCalculator/
├── Program.cs                  # ASP.NET Core minimal API host
├── VolumeCalculator.cs         # Volume formula and result record
├── TunnelBlockCalculator.csproj
├── Dockerfile                  # Container image for Render / any PaaS
├── .dockerignore
├── README.md
└── wwwroot/
    └── index.html              # Modern blue UI + Three.js 3D viewer
```

## Run locally

```bash
dotnet run
```

Then open <http://localhost:8080>.

## Run with Docker

```bash
docker build -t tunnel-block-calculator .
docker run -p 8080:8080 tunnel-block-calculator
```

## Deploy to Render

1. Push this folder to a GitHub repository.
2. On <https://render.com> create a new **Web Service** from the repo.
3. Choose **Docker** as the runtime — Render will detect the `Dockerfile`
   automatically.
4. No additional environment variables are required; Render injects `PORT`
   and the app binds to it.

## API

`POST /api/volume`

```json
{ "length": 10, "width": 4, "wallHeight": 3 }
```

Response:

```json
{
  "length": 10,
  "width": 4,
  "wallHeight": 3,
  "radius": 2,
  "totalHeight": 5,
  "rectangularVolume": 120,
  "semiCylinderVolume": 62.8319,
  "totalVolume": 182.8319,
  "formula": "V = (L × W × H) + (½ × π × (W/2)² × L)"
}
```
