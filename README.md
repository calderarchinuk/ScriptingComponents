# Scripting Components
Unity components for basic level scripting. Inspired by Events and Actions in UE3's kismet

EventComponents are called by built-in unity events(OnTriggerEnter), custom events, delegates or whatever

EventComponents 'activate' ActionComponents that do small discrete tasks

# TODO
* Descriptions on components
* Namespace
* ActionTimeline should include actions with durations (camera move, etc)
* ActionCameraMove should allow curves to lerp between a and b
* ConditionVariable should split up EvaluateConditon() so it's easier to implement blackboards, settings, etc instead of just PlayerPrefs
* ActionSetEnable should use a decorator drawer to call the 'set children' function
* PathToEditor needs to mark scene dirty, probably
* TimelineEditor make keys draggable on Timeline
* TimelineEditor reorderable list of delayed actions
* TimelineEditor variable duration instead of always 10 seconds
* TimelineEditor add more common cinematic actions
