using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    public Tilemap tilemap;
    public Node[,] grid;
    public BoundsInt bounds;

    public List<Node> path;

    private void Awake() {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;

        CreateGrid();
    }

    private void CreateGrid() {
        grid = new Node[bounds.size.x, bounds.size.y];


        for (int x = bounds.xMin, i = 0; i < bounds.size.x; x++, i++) {
            for (int y = bounds.yMin, j = 0; j < bounds.size.y; y++, j++) {
                if (tilemap.HasTile(new Vector3Int(x, y, 0))) {
                    grid[i, j] = new Node(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), true, i, j);
                }
                else {
                    grid[i, j] = new Node(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), false, i, j);
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        // check for tile above
        if (node.gridX + 1 < bounds.size.x) {
            neighbours.Add(grid[node.gridX + 1, node.gridY]);
        }

        // check for tile below
        if (node.gridX - 1 > 0) {
            neighbours.Add(grid[node.gridX - 1, node.gridY]);
        }

        // check for tile left
        if (node.gridY + 1 < bounds.size.y) {
            neighbours.Add(grid[node.gridX, node.gridY + 1]);
        }

        // check for tile right
        if (node.gridY - 1 > 0) {
            neighbours.Add(grid[node.gridX, node.gridY - 1]);
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return grid[cellPosition.x - bounds.xMin, cellPosition.y - bounds.yMin];
    }

    public Node NodeFromCellPoint(Vector3Int cellPosition) {
        return grid[cellPosition.x - bounds.xMin, cellPosition.y - bounds.yMin];
    }



}
