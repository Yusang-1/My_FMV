using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NextNode))]
public class NextNodeDrawer : PropertyDrawer
{
    private const float Spacing = 2f;

    // 선택된 전환 정책과 데이터 클래스에 맞춰 인스펙터 항목의 높이를 계산합니다.
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty transitionType = property.FindPropertyRelative(nameof(NextNode.TransitionType));
        SerializedProperty transitionData = property.FindPropertyRelative(nameof(NextNode.TransitionData));
        SerializedProperty conditions = property.FindPropertyRelative(nameof(NextNode.Conditions));
        float lineHeight = EditorGUIUtility.singleLineHeight;

        float height = lineHeight + Spacing
            + EditorGUI.GetPropertyHeight(conditions, true)
            + Spacing
            + lineHeight + Spacing;
        if (CreateTransitionData((TransitionPolicyEnum)transitionType.intValue) == null)
        {
            return height + lineHeight * 2f;
        }

        TransitionData expectedData = CreateTransitionData((TransitionPolicyEnum)transitionType.intValue);
        TransitionData currentData = transitionData.managedReferenceValue as TransitionData;
        if (currentData != null && currentData.GetType() == expectedData.GetType())
        {
            return height + EditorGUI.GetPropertyHeight(transitionData, true);
        }

        // 아직 올바른 데이터가 생성되기 전에는 새 데이터 필드의 예상 높이를 사용합니다.
        float expectedDataHeight = lineHeight;
        if (expectedData is TransitionDissolveData || expectedData is TransitionFadeData)
        {
            expectedDataHeight += lineHeight + Spacing;
        }

        return height + expectedDataHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        Rect fieldRect = new Rect(position.x, position.y, position.width, lineHeight);

        SerializedProperty nextCode = property.FindPropertyRelative(nameof(NextNode.NextCode));
        SerializedProperty conditions = property.FindPropertyRelative(nameof(NextNode.Conditions));
        SerializedProperty transitionType = property.FindPropertyRelative(nameof(NextNode.TransitionType));
        SerializedProperty transitionData = property.FindPropertyRelative(nameof(NextNode.TransitionData));

        EditorGUI.PropertyField(fieldRect, nextCode);
        fieldRect.y += lineHeight + Spacing;

        fieldRect.height = EditorGUI.GetPropertyHeight(conditions, true);
        EditorGUI.PropertyField(fieldRect, conditions, true);
        fieldRect.y += fieldRect.height + Spacing;
        fieldRect.height = lineHeight;

        int previousPolicy = transitionType.intValue;
        EditorGUI.PropertyField(fieldRect, transitionType);
        if (transitionType.intValue != previousPolicy)
        {
            // 정책이 바뀌면 이전 정책의 데이터를 새 정책에 맞는 데이터로 교체합니다.
            transitionData.managedReferenceValue = CreateTransitionData((TransitionPolicyEnum)transitionType.intValue);
            transitionData.isExpanded = true;
        }

        TransitionData expectedData = CreateTransitionData((TransitionPolicyEnum)transitionType.intValue);
        if (expectedData == null)
        {
            fieldRect.y += lineHeight + Spacing;
            fieldRect.height = lineHeight * 2f;
            EditorGUI.HelpBox(fieldRect, "No transition data is defined for this policy.", MessageType.Warning);
            EditorGUI.EndProperty();
            return;
        }

        TransitionData currentData = transitionData.managedReferenceValue as TransitionData;
        if (currentData == null || currentData.GetType() != expectedData.GetType())
        {
            // 초기값이 없거나 정책과 데이터 타입이 맞지 않는 경우 올바른 타입으로 보정합니다.
            transitionData.managedReferenceValue = expectedData;
            transitionData.isExpanded = true;
        }

        fieldRect.y += lineHeight + Spacing;
        fieldRect.height = EditorGUI.GetPropertyHeight(transitionData, true);
        EditorGUI.PropertyField(fieldRect, transitionData, new GUIContent("Transition Data"), true);

        EditorGUI.EndProperty();
    }

    private static TransitionData CreateTransitionData(TransitionPolicyEnum policy)
    {
        // 각 전환 정책을 인스펙터에서 편집할 데이터 타입에 연결합니다.
        switch (policy)
        {
            case TransitionPolicyEnum.Cut:
                return new TransitionCutData();
            case TransitionPolicyEnum.Dissolve:
                return new TransitionDissolveData();
            case TransitionPolicyEnum.Fade:
                return new TransitionFadeData();
            default:
                return null;
        }
    }
}
