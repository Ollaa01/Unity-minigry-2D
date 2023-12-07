using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class M_Board : MonoBehaviour
{
    public static M_Board Instance { get; private set; } //singleton mozliwey

    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioSource _audioSource;

    public M_Row[] rows;
    public M_Tile[,] tiles { get; private set; }

    public int Width => tiles.GetLength(0);
    public int Height=> tiles.GetLength(1);

    private M_Tile _selectedTile1;
    private M_Tile _selectedTile2;

    private readonly List<M_Tile> _selection = new List<M_Tile>();

    private const float TweenDuration = 0.25f;

    public void Awake() => Instance = this;

    void Start()
    {
        noMoves.gameObject.SetActive(false);
        tiles = new M_Tile[rows.Max(row => row.Rtiles.Length), rows.Length];

        for (var y = 0; y < Height; y++)
        {
            for (var x =0; x < Width; x++)
            {
                var tile = rows[y].Rtiles[x];
                tile.x = x;
                tile.y = y;
                tile.Item = M_ItemDatabase.Items[Random.Range(0, M_ItemDatabase.Items.Length)];
                tiles[x, y] = tile;

            }
        }

        Pop();

    }
    private bool isWaiting = false;
    private float waitStartTime;
    public Text noMoves;
    public void Update()
    {
        //if (!Input.GetKeyDown(KeyCode.A)) return;

        //foreach (var connectedTile in tiles[0, 0].GetConnectedTiles()) { connectedTile.icon.transform.DOScale(2f, TweenDuration).Play();}
        if (!HasPossibleMoves() && !isWaiting)
        {
            //Debug.Log("Brak mo¿liwych ruchów. Oczekiwanie 5 sekund przed tasowaniem planszy...");
            //Invoke("ShuffleBoard", 5f);
            // ShuffleBoard();
            //StartCoroutine(ShuffleAfterDelay());
            Debug.Log("Brak mo¿liwych ruchów. Oczekiwanie 5 sekund przed tasowaniem planszy...");
            noMoves.gameObject.SetActive(true);
            isWaiting = true;
            waitStartTime = Time.time;
        }
        if (isWaiting && Time.time - waitStartTime >= 2.5f)
        {
            ShuffleBoard();
            noMoves.gameObject.SetActive(false);
            isWaiting = false;
        }

    }

    IEnumerator ShuffleAfterDelay()
    {
        Debug.Log("Brak mo¿liwych ruchów. Oczekiwanie 5 sekund przed tasowaniem planszy...");
        yield return new WaitForSeconds(5f);

        ShuffleBoard();
    }

    public async void Select(M_Tile tile)
    {
        if (!_selection.Contains(tile)) 
        { 
             //Przesuwaj s¹siadów
            if (_selection.Count > 0)
            {
                if (System.Array.IndexOf(_selection[0].Neighbours, tile) != -1)
                {
                    Debug.Log("add tile to list " + tile);
                    _selection.Add(tile);
                }
                else
                {
                    // Jeœli klikniêty kafelek nie jest s¹siadem, zrestartuj zaznaczenie.
                    Debug.Log("zrestartowalem");
                    _selection.Clear();
                    _selection.Add(tile);
                }
            }
            else
            {
                Debug.Log("add tile to list by else " + tile);
                _selection.Add(tile);
            } 
            //_selection.Add(tile); //przesuwaj na ca³ej planszy
        }

        if (_selection.Count < 2) return;

        //Debug.Log("Select tiles at " + _selection[0].x + " " + _selection[0].y + " " + _selection[1].x + " " + _selection[1].y + " ");

        await Swap(_selection[0], _selection[1]);

        if(CanPop())
        {
            Pop();
        }
        else
        {
            await Swap(_selection[0], _selection[1]);
        }

        _selection.Clear();
    }

    public async Task Swap(M_Tile tile1, M_Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();

        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play()
                      .AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;

    }

    private bool CanPop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2 ) { return true; }
            }
        }

        return false;

    }

    private async void Pop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = tiles[x, y];

                var connectedTiles = tile.GetConnectedTiles();

                if (connectedTiles.Skip(1).Count() < 2) continue;
                
                var deflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));
                }

                _audioSource.PlayOneShot(collectSound);

                M_Score.Instance.Score += tile.Item.value * connectedTiles.Count;

                await deflateSequence.Play()
                                     .AsyncWaitForCompletion();


                var inflateSequence = DOTween.Sequence();

                foreach(var connectedTile in connectedTiles)
                {
                    connectedTile.Item = M_ItemDatabase.Items[Random.Range(0, M_ItemDatabase.Items.Length)];
                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                }

                await inflateSequence.Play()
                                       .AsyncWaitForCompletion();

                x = 0;
                y = 0;
            }
        }
    }
    public void ShuffleBoard()
    {

        List<M_Tile> allTiles = new List<M_Tile>();

        // Zbierz wszystkie kafelki na planszy do listy
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                allTiles.Add(tiles[x, y]);
            }
        }

        // Wymieszaj elementy na liœcie
        for (int i = 0; i < allTiles.Count; i++)
        {
            int randomIndex = Random.Range(i, allTiles.Count);
            M_Tile temp = allTiles[i];
            allTiles[i] = allTiles[randomIndex];
            allTiles[randomIndex] = temp;
        }

        // Przypisz nowe przedmioty do kafelków
        int index = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                tiles[x, y].Item = allTiles[index].Item;
                index++;
            }
        }
        Debug.Log("Shuffled!");
        Pop();
    }

    /*
    //Match dla przesuwaj na ca³ej planszy
    private bool HasPossibleMoves()
    {
        // SprawdŸ ka¿dy kafelek na planszy
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                M_Tile currentTile = tiles[x, y];

                // SprawdŸ ka¿dego s¹siada
                foreach (var neighbor in currentTile.Neighbours)
                {
                    // Zamieñ miejscami i sprawdŸ, czy to spowoduje utworzenie matcha
                    SwapTiles(currentTile, neighbor);
                    if (CanPop())
                    {
                        // Jeœli ruch spowoduje matcha, przywróæ oryginalne ustawienia i zwróæ true
                        SwapTiles(currentTile, neighbor); // Przywróæ oryginalne ustawienia
                        Debug.Log($"Possible match at ({currentTile.x}, {currentTile.y}) and ({neighbor.x}, {neighbor.y})");
                        return true;
                    }

                    // Jeœli ruch nie spowoduje matcha, przywróæ oryginalne ustawienia
                    SwapTiles(currentTile, neighbor);
                }
            }
        }

        // Jeœli ¿aden ruch nie spowoduje matcha, zwróæ false
        return false;
    } 
    */

    private bool HasPossibleMoves()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                M_Tile currentTile = tiles[x, y];

                // Check right neighbor
                if (x < Width - 1)
                {
                    SwapTiles(currentTile, tiles[x + 1, y]);
                    if (CanPop())
                    {
                        Debug.Log($"Possible match at ({currentTile.x}, {currentTile.y}) and ({x + 1}, {y})");
                        SwapTiles(currentTile, tiles[x + 1, y]);
                        return true;
                    }
                    SwapTiles(currentTile, tiles[x + 1, y]);
                }

                // Check down neighbor
                if (y < Height - 1)
                {
                    SwapTiles(currentTile, tiles[x, y + 1]);
                    if (CanPop())
                    {
                        Debug.Log($"Possible match at ({currentTile.x}, {currentTile.y}) and ({x}, {y + 1})");
                        SwapTiles(currentTile, tiles[x, y + 1]);
                        return true;
                    }
                    SwapTiles(currentTile, tiles[x, y + 1]);
                }

                // Check left neighbor
                if (x > 0)
                {
                    SwapTiles(currentTile, tiles[x - 1, y]);
                    if (CanPop())
                    {
                        Debug.Log($"Possible match at ({currentTile.x}, {currentTile.y}) and ({x - 1}, {y})");
                        SwapTiles(currentTile, tiles[x - 1, y]);
                        return true;
                    }
                    SwapTiles(currentTile, tiles[x - 1, y]);
                }

                // Check bottom neighbor
                if (y > 0)
                {
                    SwapTiles(currentTile, tiles[x, y - 1]);
                    if (CanPop())
                    {
                        Debug.Log($"Possible match at ({currentTile.x}, {currentTile.y}) and ({x}, {y - 1})");
                        SwapTiles(currentTile, tiles[x, y - 1]);
                        return true;
                    }
                    SwapTiles(currentTile, tiles[x, y - 1]);
                }
            }
        }

        Debug.Log("No possible matches");
        return false;
    }

    private void SwapTiles(M_Tile tile1, M_Tile tile2)
    {
        // SprawdŸ, czy oba kafelki nie s¹ nullami
        if (tile1 == null || tile2 == null)
        {
            Debug.LogWarning("One of the tiles is null. Swap aborted.");
            return;
        }

        // Zamieñ miejscami kafelki bez animacji, poniewa¿ chcemy tylko sprawdziæ, czy to spowoduje matcha
        M_Item tempItem = tile1.Item;
        tile1.Item = tile2.Item;
        tile2.Item = tempItem;
    } 





}
