using System.Diagnostics;

var source = new ActivitySource("Foo");

//没有匹配的Listener
Debug.Assert(source.CreateActivity("Bar", ActivityKind.Internal) == null);

//采样结果为None
var listener1 = new ActivityListener { ShouldListenTo = MatchAll, Sample = SampleNone };
ActivitySource.AddActivityListener(listener1);
Debug.Assert(source.CreateActivity("Bar", ActivityKind.Internal) == null);

//采样结果为PropagationData
var listener2 = new ActivityListener { ShouldListenTo = MatchAll, Sample = SamplePropagationData };
ActivitySource.AddActivityListener(listener2);
var activity = source.CreateActivity("Bar", ActivityKind.Internal);
Debug.Assert(activity?.IsAllDataRequested == false);

//采样结果为SampleAllData
var listener3 = new ActivityListener{ ShouldListenTo = MatchAll, Sample = SampleAllData };
ActivitySource.AddActivityListener(listener3);
activity = source.CreateActivity("Bar", ActivityKind.Internal);
Debug.Assert(activity?.IsAllDataRequested == true);

ActivitySamplingResult SampleNone(ref ActivityCreationOptions<ActivityContext> options)
    => ActivitySamplingResult.None;
ActivitySamplingResult SamplePropagationData(ref ActivityCreationOptions<ActivityContext> options)
    => ActivitySamplingResult.PropagationData;
ActivitySamplingResult SampleAllData(ref ActivityCreationOptions<ActivityContext> options)
    => ActivitySamplingResult.AllData;
bool MatchAll(ActivitySource activitySource) => true;
