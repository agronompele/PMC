using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace WPFClient
{
    [Export(typeof(PMCViewModel))]
    public class PMCViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        public int CountOfContainers { get; set; }
        public int CountOfMatrices { get; set; }
        public int CountOfPositions { get; set; }
        public int CountOfPoints { get; set; }
        public string Dimension { get; set; }
        public string NumericType { get; set; }
        public string Benchmark { get; set; }
        public bool CanCreateDataContainers { get; set; }
        public List<string> DimensionTypes
        {
            get
            {
                return new List<string> { "1D", "2D", "3D" };
            }
        }
        public List<string> NumericTypes
        {
            get
            {
                return new List<string> { "int", "double", "decimal" };
            }
        }

        private void ChangeButtonEnableStatus(bool status)
        {
            CanCreateDataContainers = status;
            NotifyOfPropertyChange(() => CanCreateDataContainers);
        }

        [ImportingConstructor]
        public PMCViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            CountOfContainers = 10;
            CountOfMatrices = 10;
            CountOfPositions = 10;
            CountOfPoints = 10;
            Dimension = DimensionTypes[0];
            NumericType = NumericTypes[0];
            CanCreateDataContainers = true;
        }
        public async void CreateDataContainers()
        {
            ChangeButtonEnableStatus(false);

            var parameters = new CreationParameters
            {
                CountOfContainers = this.CountOfContainers,
                CountOfMatrices = this.CountOfMatrices,
                CountOfPositions = this.CountOfPositions,
                CountOfPoints = this.CountOfPoints,
                Dimension = this.Dimension,
                NumericType = this.NumericType
            };

            var sw = new Stopwatch();
            sw.Start();

            await Task.Run(
                () => DataGenerator.GenerateContainerCollection(parameters));

            sw.Stop();
            Benchmark = sw.Elapsed.ToString();
            NotifyOfPropertyChange(() => Benchmark);

            ChangeButtonEnableStatus(true);
        }
    }
}
