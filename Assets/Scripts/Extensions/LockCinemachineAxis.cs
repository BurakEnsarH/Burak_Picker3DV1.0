using System;
using Cinemachine;
using Enums;
using UnityEngine;

namespace Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")] [SerializeField]
        private float m_Position = 0f;

        [SerializeField] private CinemachineStates cinemachineState;
        [SerializeField] private CinemachineAxis axis;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (cinemachineState)
            {
                case CinemachineStates.Body:
                {
                    switch (axis)
                    {
                        case CinemachineAxis.XAxis:
                        {
                            var pos = state.RawPosition;
                            pos.x = m_Position;
                            state.RawPosition = pos;
                            break;
                        }
                        case CinemachineAxis.YAxis:
                        {
                            var pos = state.RawPosition;
                            pos.y = m_Position;
                            state.RawPosition = pos;
                            break;
                        }
                        case CinemachineAxis.ZAxis:
                        {
                            var pos = state.RawPosition;
                            pos.z = m_Position;
                            state.RawPosition = pos;
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
                case CinemachineStates.Aim:
                {
                    switch (axis)
                    {
                        case CinemachineAxis.XAxis:
                        {
                            var pos = state.RawOrientation;
                            pos.x = m_Position;
                            state.RawOrientation = pos;
                            break;
                        }
                        case CinemachineAxis.YAxis:
                        {
                            var pos = state.RawOrientation;
                            pos.y = m_Position;
                            state.RawOrientation = pos;
                            break;
                        }
                        case CinemachineAxis.ZAxis:
                        {
                            var pos = state.RawOrientation;
                            pos.z = m_Position;
                            state.RawOrientation = pos;
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
                    break;
                case CinemachineStates.Noise:
                    break;
                case CinemachineStates.Finalize:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}