using System.Diagnostics;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFLife
{
    public class LifeCanvas : System.Windows.Controls.Canvas
    {
        // WINDOW SIZE = 440 x 800, GAME SIZE = 400 x 400
        private int _gridSize = 40;

        public int GRIDSIZE
        {
            get { return _gridSize;  } 
            set
            {     
                _gridSize = value;
                CELLWIDTH = 400 / _gridSize;
            }
        } // How many cells to render

        private int CELLWIDTH = 10; // Size of each cell, dependent on gridsize

        // Simulation speed determined by slider, it's the delay in MS between each call to update.
        public int SIMSPEED = 100;

        // Current cell set
        private Dictionary<Vector2, Cell> cells = new Dictionary<Vector2, Cell>();

        // Determines if can continue playing or if it has been paused
        private bool playing = true;

        // Initial constructor is wired to immedetialy randomize and begin the simulation on startup
        public LifeCanvas()
        {
            InitializeRandomCells();
            ApplyRules();
        }

        private void InitializeDeadCells()
        {
            Children.Clear();
            cells.Clear();

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    Cell new_cell = new();
                    new_cell.Resize(CELLWIDTH + 1, CELLWIDTH + 1);

                    Vector2 new_coords = new Vector2(i, j);
                    new_cell.coords = new_coords;
                    this.cells.Add(new_coords, new_cell);

                    Children.Add(new_cell);

                    SetLeft(new_cell, j * CELLWIDTH);
                    SetTop(new_cell, i * CELLWIDTH);
                }
            }
        }

        private void InitializeRandomCells()
        {
            Children.Clear();
            cells.Clear();

            Random random = new Random(); // For debugging

            Debug.WriteLine(CELLWIDTH);

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    Cell new_cell = new();
                    new_cell.Resize(CELLWIDTH + 1, CELLWIDTH + 1); // + 1 to remove artifact pixels via slight overlap

                    Vector2 new_coords = new Vector2(i, j);
                    new_cell.coords = new_coords;
                    this.cells.Add(new_coords, new_cell);

                    Children.Add(new_cell);

                    // For randomly setting firstt living cells
                    int result = random.Next();
                    if (result % 3 == 0)
                    {
                        new_cell.Revive();
                    }

                    SetLeft(new_cell, j * CELLWIDTH);
                    SetTop(new_cell, i * CELLWIDTH);
                }
            }
        }

        public void ResetGrid()
        {
            Pause();
            InitializeDeadCells();
        }

        public void RandomizeGrid()
        {
            Pause();
            InitializeRandomCells();
        }

        public bool IsPlaying()
        {
            return playing;
        }

        public void PlayOrPause()
        {
            if (playing)
            { 
                Pause();
            } else
            {
                Play();
            }
        }

        private void Pause()
        {
            this.playing = false;
        }

        private void Play()
        {
            if (!this.playing)
            {
                this.playing = true;
                ApplyRules(); // Restart recursive call on play pressed
            }
        }

        private async void ApplyRules()
        {
            // Rules must be applied to all cells at the same time, so mark cells for updates and then apply the update to all after
            foreach (var cell in cells.Values)
            {
                // Check if a living cell should die
                if (cell.IsAlive())
                {
                    cell.CheckDeathConditions(cells);
                }
                else // Check if a new cell should be born
                {
                    cell.Birth(cells);
                }
            }

            // Apply updates to all simultaneously
            foreach (var cell in cells.Values)
            {
                if (cell.IsAlive())
                {
                    if (cell.killNextUpdate)
                    {
                        cell.Kill();
                        cell.killNextUpdate = false;
                    }
                }
                else
                {
                    if (cell.reviveNextUpdate)
                    {
                        cell.Revive();
                        cell.reviveNextUpdate = false;
                    }
                }
            }

            await Task.Delay(SIMSPEED);

            if (this.playing)
            {
                ApplyRules();
            }
        }


        // Cell class is essentially a rectangle that knows its own position. LifeCanvas is responsible for its lifecycle.
        // Drawing functionality is handled by a simple onclick method which toggles its state.
        public class Cell : UserControl
        {
            // State of life
            private bool alive = false;

            // Marked for death / rebirth
            public bool killNextUpdate = false;
            public bool reviveNextUpdate = false;

            public Vector2 coords = new Vector2();

            private Rectangle rect;

            public Cell()
            {
                // All cells begin dead
                this.rect = new Rectangle();
                this.rect.Fill = Brushes.Black;
                this.rect.MouseLeftButtonDown += ToggleLife; // Toggle life on click
                // Cells are resized by LifeCanvas based on the user selected grid size
                this.Content = this.rect;
            }

            public void Resize(int width, int height)
            {
                this.rect.Width = width;
                this.rect.Height = height;
            }

            public bool IsAlive()
            {
                return alive;
            }

            public void Kill()
            {
                alive = false;
                this.rect.Fill = Brushes.Black;
            }

            public void Revive()
            {
                alive = true;
                this.rect.Fill = Brushes.White;
            }

            private void ToggleLife(object sender, MouseButtonEventArgs e)
            {
                if (alive)
                {
                    Kill();
                }
                else
                {
                    Revive();
                }
            }

            private int GetAdjacentNeighborCount(Dictionary<Vector2, Cell> cells)
            {
                List<Vector2> adjacent =
                [
                    this.coords + new Vector2(-1,-1), // Top Left
                    this.coords + new Vector2(0, -1), // Top middle
                    this.coords + new Vector2(1, -1), // Top right
                    this.coords + new Vector2(-1, 0), // Left
                    this.coords + new Vector2(1, 0), // Right
                    this.coords + new Vector2(-1, 1), // Bottom left 
                    this.coords + new Vector2(0, 1), // Bottom middle
                    this.coords + new Vector2(1, 1), // Bottom right
                ];

                adjacent = adjacent.Where(n => cells.ContainsKey(n)).ToList(); // For filtering out non-existent cells

                int total_neighbors = 0;
                foreach (Vector2 neighbor_coord in adjacent)
                {
                    if (cells[neighbor_coord].IsAlive())
                    {
                        total_neighbors++;
                    }
                }

                return total_neighbors;
            }

            // Rule 1, a dead cell becomes alive if it has exactly 3 neighbors
            public void Birth(Dictionary<Vector2, Cell> cells)
            {
                if (GetAdjacentNeighborCount(cells) == 3)
                {
                    this.reviveNextUpdate = true;
                }
            }
            
            // Implemented to combine rules 2 and 3 (for efficiency)
            public void CheckDeathConditions(Dictionary<Vector2, Cell> cells)
            {
                int total_neighbors = GetAdjacentNeighborCount(cells);
                Isolation(total_neighbors);
                Overcrowding(total_neighbors);
            }

            // Rule 2, A cell dies if it has one or fewer neighbors
            public void Isolation(int neighborCount)
            {
                if (neighborCount <= 1)
                {
                    this.killNextUpdate = true;
                }
            }

            // Rule 3, A Cell dies if it has four or more neighbors
            public void Overcrowding(int neighborCount)
            {
                if (neighborCount >= 4)
                {
                    this.killNextUpdate = true;
                }
            }

            // So a cell lives if it has 2 or 3 live neighbors. 
        }
    }
}
