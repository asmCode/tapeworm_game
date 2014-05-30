using UnityEngine;
using System.Collections;

public delegate void EventHandler(object sender);

public delegate void EventHandler<EventDataType>(object sender, EventDataType data);
