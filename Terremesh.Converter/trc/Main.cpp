#include "Terremesh/Required.h"

#include "Terremesh/Remesh/MeshReader.h"
#include "Terremesh/Remesh/Mesh.h"
#include "Terremesh/Remesh/MeshWriter.h"
#include "Terremesh/IProgressListener.h"

#include "Terremesh/QuadricErrorMetric/QuadricErrorMetricMethod.h"

class ConsoleProgressListener 
	: public Terremesh::IProgressListener
{
public:
	virtual void OnStarted(const std::string& stage)
	{
		std::cout << "[";
		std::cout.width(10);
		std::cout << (int)clock();
		std::cout.width(1);
		std::cout << "]: ";
		
		std::cout << stage << " [started]" << std::endl;
	}

	virtual void OnCompleted(const std::string& stage)
	{
		std::cout << "[";
		std::cout.width(10);
		std::cout << (int)clock();
		std::cout.width(1);
		std::cout << "]: ";
		std::cout << stage << " [completed]" << std::endl;
		m_Progress = 0;
	}

	virtual void OnStep(int current, int total)
	{
		float ratio = (float)current / (float)total;
		ratio *= 100.0;

		int iratio = (int)ratio;

		if (iratio != m_Progress)
		{
			m_Progress = iratio;

			std::cout << "[";
			std::cout.width(10);
			std::cout << (int)clock();
			std::cout.width(1);
			std::cout << "]: ";
			std::cout << m_Progress << "%" << std::endl;
		}
		//std::cout << current << "/" << total << std::endl;
	}

private:
	int m_Progress;
};

#include "Terremesh/optionparser.h"

enum OptionIndex
{
	OptionIndex_Unknown,
	OptionIndex_Input,
	OptionIndex_Output,
	OptionIndex_Percent,
	OptionIndex_Target,
	OptionIndex_Method,
	OptionIndex_Help,
};

const option::Descriptor usage[] = 
{
	{OptionIndex_Unknown, 0, "", "", option::Arg::None, "Usage: trc [options]\n\n"},
	{OptionIndex_Help, 0, "", "help", option::Arg::None,			"  --help              Print usage and exit"},
	{OptionIndex_Input, 0, "i", "input", option::Arg::Optional,		"  --input=FILEPATH    Sets input file name"},
	{OptionIndex_Output, 0, "o", "output", option::Arg::Optional,	"  --output=FILEPATH   Sets output file name"},
	{OptionIndex_Percent, 0, "r", "ratio", option::Arg::Optional,   "  --ratio=RATIO       Sets removed triangles ratio"},
	{OptionIndex_Target, 0, "t", "target", option::Arg::Optional,   "  --target=TRIANGLES  Sets target number of triangles"},
	{OptionIndex_Method, 0, "m", "method", option::Arg::Optional,   "  --method=METHOD     Sets used method"},
	{0, 0, 0, 0, 0, 0},
};

int main(int argc, char* argv[])
{
	std::cout
		<< "Karol Grzybowski & Piotr Ruchwa" << std::endl
		<< "(C) 2012" << std::endl
		<< "Terremesh.Converter" << std::endl
		<< std::endl;

#if 1
	argc -= (argc > 0) ? 1 : 0;
	argv += (argc > 0) ? 1 : 0;

	option::Stats stats(usage, argc, argv);
	option::Option* options = new option::Option[stats.options_max];
	option::Option* buffer = new option::Option[stats.buffer_max];

	option::Parser parser(usage, argc, argv, options, buffer);

	if (parser.error())
	{
		std::cerr << "Cannot parser args" << std::endl;
		return -1;
	}

	if (options[OptionIndex_Help] || argc == 0)
	{
		option::printUsage(std::cout, usage);
		return 0;
	}

	if (options[OptionIndex_Input].arg == nullptr)
	{
		std::cerr << "No input specified" << std::endl;
		return -1;
	}

	if (options[OptionIndex_Output].arg == nullptr)
	{
		std::cerr << "No output specified" << std::endl;
		return -1;
	}

	if ((options[OptionIndex_Target].arg == nullptr) && (options[OptionIndex_Percent].arg == nullptr))
	{
		std::cerr << "No ratio or target specified" << std::endl;
		return -1;
	}

	if (options[OptionIndex_Method].arg == nullptr)
	{
		std::cerr << "No method specified" << std::endl;
		return -1;
	}

	const char* inputFilePath = options[OptionIndex_Input].arg;
	const char* outputFilePath = options[OptionIndex_Output].arg;

	bool hasRatio = options[OptionIndex_Percent].arg != nullptr;
	float ratio = 0.5;
	int target = 2;

	if (hasRatio)
	{
		ratio = (double)atof(options[OptionIndex_Percent].arg);
	}
	else
	{
		target = atol(options[OptionIndex_Target].arg);
	}
#else
	
	auto inputFilePath = "../canyon.obj";
	auto outputFilePath = "../out.obj";
	auto hasRatio = true;
	auto ratio = 0.3;
	auto target = 0;
#endif

	std::ifstream iStream(inputFilePath);
	std::ofstream oStream(outputFilePath);

	ConsoleProgressListener listener;

	Terremesh::Remesh::Mesh mesh;
	Terremesh::Remesh::MeshReader reader(iStream);
	Terremesh::Remesh::MeshWriter writer(oStream);

	Terremesh::QuadricErrorMetric::QuadricErrorMetricMethod method;
	reader.Read(mesh, &listener);
	if (hasRatio)
	{
		method.Process(mesh, ratio, &listener);
	}
	else
	{
		method.Process(mesh, target, &listener);
	}
	writer.Write(mesh, &listener);

	return 0;
}
