# Fase 2 — Pacote de retorno da campanha de prova técnica

Gerado em: 2026-09-08T22:47:10.1557825-03:00
Commit-base: `e9ead73`
Escopo: 234 Feature Specs em 20 domínios; prototypes isolados, sem backend final.

## A–D. Artefatos e estado do Git

- Feature Specs finais: `/feature-specs/`
- Prototypes e resultados: `/prototypes/`
- Pacote canônico: `/PHASE-2-RETURN-PACKAGE.md`; cópia gerada também em `/prototypes/system/C-SYSTEM-017/results/PHASE-2-RETURN-PACKAGE.md`.
- Matriz canônica: `/PHASE-2-PROOF-MATRIX.md`; cópia gerada também em `/prototypes/system/C-SYSTEM-017/results/PHASE-2-PROOF-MATRIX.md`.
- Diff auditável após o commit da campanha: `git diff e9ead73..HEAD -- .gitignore feature-specs prototypes PHASE-2-RETURN-PACKAGE.md PHASE-2-PROOF-MATRIX.md`. O range inclui arquivos adicionados, modificados e resultados persistidos.
- `docs/02A-PROTOTYPING-TECHNICAL-PROOF.md` já estava não rastreado antes da campanha e foi preservado como entrada operacional.

### git status --short capturado

```text
 M feature-specs/audio/C-AUDIO-001-audio-device-and-endpoint-inventory.md
 M feature-specs/audio/C-AUDIO-002-per-app-routing-sessions-and-mixer-diagnostics.md
 M feature-specs/audio/C-AUDIO-003-audio-driver-and-device-health.md
 M feature-specs/audio/C-AUDIO-004-audio-format-modes-and-processing.md
 M feature-specs/audio/C-AUDIO-005-spatial-channel-and-speaker-configuration.md
 M feature-specs/audio/C-AUDIO-006-digital-usb-and-bluetooth-audio-paths.md
 M feature-specs/audio/C-AUDIO-007-microphone-diagnostics-and-validation.md
 M feature-specs/audio/C-AUDIO-008-audio-glitch-latency-and-contention-diagnostics.md
 M feature-specs/audio/C-AUDIO-009-audio-repair-workflow.md
 M feature-specs/audio/C-AUDIO-010-virtual-audio-and-capture-path-awareness.md
 M feature-specs/audio/C-AUDIO-011-audio-quality-comparison-and-outcome-validation.md
 M feature-specs/audio/C-AUDIO-013-audio-sleep-resume-diagnostics.md
 M feature-specs/cleaning/C-CLEANING-001-cleanup-opportunity-assessment.md
 M feature-specs/cleaning/C-CLEANING-002-safe-temporary-and-rebuildable-data-cleanup.md
 M feature-specs/cleaning/C-CLEANING-003-user-file-review-and-space-recovery.md
 M feature-specs/cleaning/C-CLEANING-004-application-and-installation-residue-review.md
 M feature-specs/cleaning/C-CLEANING-005-logs-dumps-and-diagnostic-artifact-retention.md
 M feature-specs/cleaning/C-CLEANING-006-cleanup-safety-provenance-and-exclusions.md
 M feature-specs/cleaning/C-CLEANING-007-cleanup-preview-execution-safety-and-verification.md
 M feature-specs/cleaning/C-CLEANING-008-cleanup-policy-and-storage-sense-awareness.md
 M feature-specs/cleaning/C-CLEANING-009-multi-drive-cleanup-awareness.md
 M feature-specs/cpu/C-CPU-001-cpu-identity-capabilities-and-topology.md
 M feature-specs/cpu/C-CPU-002-cpu-benchmark-suite.md
 M feature-specs/cpu/C-CPU-003-cpu-utilization-and-saturation-diagnostics.md
 M feature-specs/cpu/C-CPU-004-cpu-bottleneck-and-performance-classification.md
 M feature-specs/cpu/C-CPU-005-cpu-clocks-boost-and-performance-state-diagnostics.md
 M feature-specs/cpu/C-CPU-006-cpu-thermal-power-and-limiter-diagnostics.md
 M feature-specs/cpu/C-CPU-007-cpu-scheduler-and-workload-placement-diagnostics.md
 M feature-specs/cpu/C-CPU-009-cpu-pmu-and-hardware-counter-diagnostics.md
 M feature-specs/cpu/C-CPU-010-cpu-firmware-microcode-and-security-context.md
 M feature-specs/cpu/C-CPU-011-cpu-optimization-myth-and-unsafe-tweak-guards.md
 M feature-specs/cpu/C-CPU-013-cpu-vendor-specific-capability-abstraction.md
 M feature-specs/cpu/C-CPU-014-cpu-stability-and-hardware-error-validation.md
 M feature-specs/cpu/C-CPU-015-cpu-monitoring-modes-and-overhead-safeguards.md
 M feature-specs/display/C-DISPLAY-001-display-inventory-edid-and-path-mapping.md
 M feature-specs/display/C-DISPLAY-002-resolution-refresh-rate-and-mode-validation.md
 M feature-specs/display/C-DISPLAY-003-hdr-and-advanced-color-diagnostics.md
 M feature-specs/display/C-DISPLAY-004-vrr-tearing-and-motion-diagnostics.md
 M feature-specs/display/C-DISPLAY-005-multi-monitor-topology-and-compatibility.md
 M feature-specs/display/C-DISPLAY-006-scaling-dpi-and-color-profile-diagnostics.md
 M feature-specs/display/C-DISPLAY-007-display-link-connection-and-hotplug-diagnostics.md
 M feature-specs/display/C-DISPLAY-008-panel-visual-test-tools.md
 M feature-specs/display/C-DISPLAY-009-display-power-and-brightness-behavior.md
 M feature-specs/display/C-DISPLAY-011-display-change-safety-rollback-and-validation.md
 M feature-specs/display/C-DISPLAY-012-external-monitor-control-and-firmware-awareness.md
 M feature-specs/display/C-DISPLAY-013-virtual-remote-display-awareness.md
 M feature-specs/drivers/C-DRIVERS-001-driver-inventory-mapping-and-metadata.md
 M feature-specs/drivers/C-DRIVERS-002-driver-device-problem-diagnostics.md
 M feature-specs/drivers/C-DRIVERS-003-driver-update-and-source-awareness.md
 M feature-specs/drivers/C-DRIVERS-004-driver-install-rollback-and-restart-workflow.md
 M feature-specs/drivers/C-DRIVERS-005-driver-safety-and-critical-device-safeguards.md
 M feature-specs/drivers/C-DRIVERS-006-driver-store-maintenance.md
 M feature-specs/drivers/C-DRIVERS-007-driver-regression-correlation-and-validation.md
 M feature-specs/drivers/C-DRIVERS-008-driver-change-history-backup-and-explanation.md
 M feature-specs/drivers/C-DRIVERS-009-driver-installation-policy-awareness.md
 M feature-specs/energy/C-ENERGY-001-power-scheme-inventory-switching-and-state.md
 M feature-specs/energy/C-ENERGY-003-processor-power-policy-diagnostics-and-tuning.md
 M feature-specs/energy/C-ENERGY-004-energy-efficiency-and-performance-per-watt-analysis.md
 M feature-specs/energy/C-ENERGY-005-sleep-wake-hibernate-and-fast-startup-diagnostics.md
 M feature-specs/energy/C-ENERGY-006-device-power-management-diagnostics.md
 M feature-specs/energy/C-ENERGY-007-display-energy-management-diagnostics.md
 M feature-specs/energy/C-ENERGY-009-energy-state-history-conflict-and-outcome-validation.md
 M feature-specs/energy/C-ENERGY-010-application-process-energy-attribution-and-policy.md
 M feature-specs/energy/C-ENERGY-011-battery-capability-health-and-drain-diagnostics.md
 M feature-specs/energy/C-ENERGY-012-energy-saver-diagnostics-and-configuration.md
 M feature-specs/energy/C-ENERGY-013-timer-resolution-energy-diagnostics.md
 M feature-specs/gaming/C-GAMING-001-game-discovery-identity-and-session-context.md
 M feature-specs/gaming/C-GAMING-002-windows-gaming-configuration-diagnostics.md
 M feature-specs/gaming/C-GAMING-003-overlay-hook-mod-and-compatibility-interference.md
 M feature-specs/gaming/C-GAMING-004-presentation-mode-refresh-vrr-and-frame-cap-diagnostics.md
 M feature-specs/gaming/C-GAMING-005-upscaling-frame-generation-and-latency-technologies.md
 M feature-specs/gaming/C-GAMING-006-gaming-performance-monitoring-and-stutter-analysis.md
 M feature-specs/gaming/C-GAMING-007-game-crash-hang-and-dependency-health.md
 M feature-specs/gaming/C-GAMING-008-per-game-optimization-profiles.md
 M feature-specs/gaming/C-GAMING-009-gaming-benchmark-and-before-after-validation.md
 M feature-specs/gaming/C-GAMING-011-game-update-config-save-change-correlation.md
 M feature-specs/gaming/C-GAMING-012-gaming-hardware-context-awareness.md
 M feature-specs/gaming/C-GAMING-013-online-cloud-gaming-network-correlation.md
 M feature-specs/gaming/C-GAMING-014-gaming-audio-input-accessibility-safeguards.md
 M feature-specs/gpu/C-GPU-001-gpu-identity-inventory-and-architecture.md
 M feature-specs/gpu/C-GPU-002-gpu-memory-capacity-usage-and-pressure.md
 M feature-specs/gpu/C-GPU-003-gpu-utilization-clocks-and-telemetry.md
 M feature-specs/gpu/C-GPU-004-gpu-thermal-power-and-limiter-diagnostics.md
 M feature-specs/gpu/C-GPU-005-gpu-driver-health-and-crash-diagnostics.md
 M feature-specs/gpu/C-GPU-006-graphics-api-and-rendering-capability-inventory.md
 M feature-specs/gpu/C-GPU-007-gpu-scheduling-and-presentation-diagnostics.md
 M feature-specs/gpu/C-GPU-008-per-app-gpu-selection-and-hybrid-graphics-routing.md
 M feature-specs/gpu/C-GPU-009-resizable-bar-readiness-and-context.md
 M feature-specs/gpu/C-GPU-010-gpu-pcie-link-and-transfer-diagnostics.md
 M feature-specs/gpu/C-GPU-011-gpu-bottleneck-frametime-and-stutter-diagnostics.md
 M feature-specs/gpu/C-GPU-012-shader-cache-diagnostics-and-safeguards.md
 M feature-specs/gpu/C-GPU-013-gpu-benchmark-suite.md
 M feature-specs/gpu/C-GPU-014-gpu-stability-and-tuning-validation.md
 M feature-specs/gpu/C-GPU-015-gpu-vendor-capability-and-profile-layer.md
 M feature-specs/hardware/C-HARDWARE-001-hardware-inventory-and-firmware-context.md
 M feature-specs/hardware/C-HARDWARE-002-cross-device-health-and-error-correlation.md
 M feature-specs/hardware/C-HARDWARE-003-cross-component-hardware-stability-assessment.md
 M feature-specs/hardware/C-HARDWARE-004-cross-component-topology-and-connection-sanity-assessment.md
 M feature-specs/hardware/C-HARDWARE-005-thermal-and-power-delivery-fault-evidence.md
 M feature-specs/hardware/C-HARDWARE-006-hardware-stress-test-orchestration.md
 M feature-specs/hardware/C-HARDWARE-007-bottleneck-vs-hardware-fault-classification.md
 M feature-specs/hardware/C-HARDWARE-008-hardware-escalation-vendor-tool-and-physical-check-guidance.md
 M feature-specs/hardware/C-HARDWARE-009-hardware-diagnostic-session-evidence-bundle.md
 M feature-specs/network/C-NETWORK-001-network-adapters-active-path-and-baseline.md
 M feature-specs/network/C-NETWORK-002-connectivity-reachability-and-route-diagnostics.md
 M feature-specs/network/C-NETWORK-003-latency-jitter-loss-and-retransmission-diagnostics.md
 M feature-specs/network/C-NETWORK-004-dns-and-name-resolution-diagnostics.md
 M feature-specs/network/C-NETWORK-005-ethernet-link-and-advanced-adapter-diagnostics.md
 M feature-specs/network/C-NETWORK-006-wi-fi-capability-radio-and-roaming-diagnostics.md
 M feature-specs/network/C-NETWORK-007-network-driver-device-health.md
 M feature-specs/network/C-NETWORK-008-tcp-ip-stack-and-transport-diagnostics.md
 M feature-specs/network/C-NETWORK-009-firewall-profile-filter-and-policy-diagnostics.md
 M feature-specs/network/C-NETWORK-010-traffic-bandwidth-and-bufferbloat-diagnostics.md
 M feature-specs/network/C-NETWORK-011-vpn-proxy-tunnel-and-virtual-network-awareness.md
 M feature-specs/network/C-NETWORK-012-network-power-management-and-sleep-wake-correlation.md
 M feature-specs/network/C-NETWORK-013-network-history-drift-and-regression-correlation.md
 M feature-specs/network/C-NETWORK-014-staged-network-repair-and-rollback.md
 M feature-specs/network/C-NETWORK-016-mobile-broadband-and-hotspot-awareness.md
 M feature-specs/privacy/C-PRIVACY-001-windows-privacy-baseline-and-managed-state-awareness.md
 M feature-specs/privacy/C-PRIVACY-002-diagnostics-personalization-and-advertising-controls.md
 M feature-specs/privacy/C-PRIVACY-003-location-and-sensitive-device-permissions.md
 M feature-specs/privacy/C-PRIVACY-004-activity-speech-and-search-cloud-privacy.md
 M feature-specs/privacy/C-PRIVACY-005-find-my-device-privacy-trade-off.md
 M feature-specs/privacy/C-PRIVACY-006-privacy-change-history-and-drift-awareness.md
 M feature-specs/processes/C-PROCESSES-001-process-inventory-tree-and-app-grouping.md
 M feature-specs/processes/C-PROCESSES-002-per-process-resource-usage-and-trend-diagnostics.md
 M feature-specs/processes/C-PROCESSES-003-process-anomaly-and-root-cause-correlation.md
 M feature-specs/processes/C-PROCESSES-004-process-responsiveness-crash-and-lifecycle-diagnostics.md
 M feature-specs/processes/C-PROCESSES-005-process-scheduling-priority-and-policy-diagnostics.md
 M feature-specs/processes/C-PROCESSES-006-process-handles-threads-and-contention-diagnostics.md
 M feature-specs/processes/C-PROCESSES-007-process-action-workflow-and-safeguards.md
 M feature-specs/ram/C-RAM-001-memory-capacity-modules-and-topology-inventory.md
 M feature-specs/ram/C-RAM-002-spd-jedec-xmp-expo-profile-diagnostics.md
 M feature-specs/ram/C-RAM-003-ecc-and-physical-memory-error-diagnostics.md
 M feature-specs/ram/C-RAM-004-memory-usage-pressure-and-commit-diagnostics.md
 M feature-specs/ram/C-RAM-005-pagefile-and-crash-dump-configuration-diagnostics.md
 M feature-specs/ram/C-RAM-006-per-process-memory-and-leak-diagnostics.md
 M feature-specs/ram/C-RAM-007-kernel-pool-and-driver-leak-diagnostics.md
 M feature-specs/ram/C-RAM-008-standby-cache-compression-diagnostics.md
 M feature-specs/ram/C-RAM-009-memory-manager-tweak-default-state-safeguards.md
 M feature-specs/ram/C-RAM-010-paging-and-hard-fault-diagnostics.md
 M feature-specs/ram/C-RAM-011-memory-benchmark-suite.md
 M feature-specs/ram/C-RAM-012-numa-and-reserved-memory-diagnostics.md
 M feature-specs/ram/C-RAM-013-memory-stability-testing-and-validation.md
 M feature-specs/ram/C-RAM-014-memory-capacity-adequacy-and-upgrade-guidance.md
 M feature-specs/ram/C-RAM-015-memory-controller-voltage-and-training-context.md
 M feature-specs/ram/C-RAM-016-large-page-capability-and-application-guidance.md
 M feature-specs/ram/C-RAM-017-memory-bound-workload-diagnostics.md
 M feature-specs/repair/C-REPAIR-001-system-repair-triage-and-orchestration.md
 M feature-specs/repair/C-REPAIR-002-system-file-and-component-store-repair.md
 M feature-specs/repair/C-REPAIR-003-boot-startup-and-recovery-environment-repair.md
 M feature-specs/repair/C-REPAIR-004-pending-operation-and-restart-resolution.md
 M feature-specs/repair/C-REPAIR-005-application-and-microsoft-store-repair.md
 M feature-specs/repair/C-REPAIR-006-subsystem-repair-paths.md
 M feature-specs/repair/C-REPAIR-007-device-and-driver-repair-path.md
 M feature-specs/repair/C-REPAIR-008-repair-verification-history-and-known-good-correlation.md
 M feature-specs/repair/C-REPAIR-009-repair-escalation-source-availability-and-safeguards.md
 M feature-specs/repair/C-REPAIR-010-repair-confidence-and-rollback-readiness.md
 M feature-specs/security/C-SECURITY-001-windows-security-baseline.md
 M feature-specs/security/C-SECURITY-002-endpoint-protection-and-firewall-state.md
 M feature-specs/security/C-SECURITY-003-platform-boot-and-hardware-backed-security.md
 M feature-specs/security/C-SECURITY-004-virtualization-based-and-credential-protections.md
 M feature-specs/security/C-SECURITY-005-reputation-application-and-exploit-protection.md
 M feature-specs/security/C-SECURITY-006-uac-encryption-and-sign-in-protection.md
 M feature-specs/security/C-SECURITY-007-security-update-and-support-exposure.md
 M feature-specs/security/C-SECURITY-008-security-policy-tamper-and-unsafe-tweak-diagnostics.md
 M feature-specs/security/C-SECURITY-010-security-recovery-readiness-and-drift-history.md
 M feature-specs/startup/C-STARTUP-001-startup-entry-inventory-and-source-coverage.md
 M feature-specs/startup/C-STARTUP-002-startup-impact-timing-and-responsiveness-diagnostics.md
 M feature-specs/startup/C-STARTUP-003-startup-entry-identity-role-and-intent-context.md
 M feature-specs/startup/C-STARTUP-004-startup-disable-scoring-and-control.md
 M feature-specs/startup/C-STARTUP-005-startup-drift-regression-and-before-after-validation.md
 M feature-specs/startup/C-STARTUP-006-broken-recreated-startup-entry-diagnostics.md
 M feature-specs/startup/C-STARTUP-007-startup-dependency-and-safety-safeguards.md
 M feature-specs/startup/C-STARTUP-008-startup-backup-rollback-and-optimization-sessions.md
 M feature-specs/storage/C-STORAGE-001-storage-hardware-inventory-interface-context.md
 M feature-specs/storage/C-STORAGE-002-storage-health-smart-nvme-wear-diagnostics.md
 M feature-specs/storage/C-STORAGE-003-storage-temperature-thermal-throttling.md
 M feature-specs/storage/C-STORAGE-004-capacity-volumes-partitions-free-space-diagnostics.md
 M feature-specs/storage/C-STORAGE-005-filesystem-health-chkdsk-allocation-diagnostics.md
 M feature-specs/storage/C-STORAGE-006-trim-retrim-media-aware-optimization.md
 M feature-specs/storage/C-STORAGE-007-storage-write-cache-flush-policy-diagnostics.md
 M feature-specs/storage/C-STORAGE-008-storage-driver-controller-link-health.md
 M feature-specs/storage/C-STORAGE-009-storage-i-o-performance-saturation-diagnostics.md
 M feature-specs/storage/C-STORAGE-010-storage-benchmark-suite-methodology.md
 M feature-specs/storage/C-STORAGE-011-cleanup-storage-space-artifacts-safeguards.md
 M feature-specs/storage/C-STORAGE-012-system-restore-vss-shadow-storage-diagnostics.md
 M feature-specs/storage/C-STORAGE-013-bitlocker-storage-context-safeguards.md
 M feature-specs/storage/C-STORAGE-014-storage-spaces-pools-virtual-disks-tiers.md
 M feature-specs/storage/C-STORAGE-015-directstorage-bypassio-readiness-diagnostics.md
 M feature-specs/storage/C-STORAGE-016-game-storage-placement-guidance.md
 M feature-specs/storage/C-STORAGE-017-storage-optimization-outcome-validation-risk-guard.md
 M feature-specs/storage/C-STORAGE-018-nvme-power-management-diagnostics.md
 M feature-specs/storage/C-STORAGE-019-ssd-architecture-endurance-context.md
 M feature-specs/storage/C-STORAGE-020-storage-unsafe-low-value-optimization-guard.md
 M feature-specs/storage/C-STORAGE-021-reserved-storage-awareness.md
 M feature-specs/system/C-SYSTEM-001-system-identity-baseline-and-compatibility-context.md
 M feature-specs/system/C-SYSTEM-002-system-health-bottleneck-and-slow-pc-diagnostic-session.md
 M feature-specs/system/C-SYSTEM-003-shared-recommendation-and-explanation-capability.md
 M feature-specs/system/C-SYSTEM-006-services-and-scheduled-task-health.md
 M feature-specs/system/C-SYSTEM-007-system-change-history-drift-and-anomaly-detection.md
 M feature-specs/system/C-SYSTEM-008-windows-feature-and-core-component-health.md
 M feature-specs/system/C-SYSTEM-011-system-event-crash-and-incident-correlation.md
 M feature-specs/system/C-SYSTEM-012-performance-file-registry-tracing-and-failed-i-o-diagnostics.md
 M feature-specs/system/C-SYSTEM-013-clean-boot-and-shell-module-diagnostics.md
 M feature-specs/system/C-SYSTEM-014-pending-reboot-file-operation-diagnostics.md
 M feature-specs/system/C-SYSTEM-015-shared-benchmark-readiness-assessment.md
 M feature-specs/system/C-SYSTEM-016-accessibility-safeguard.md
 M feature-specs/system/C-SYSTEM-017-shared-diagnostic-evidence-correlation-and-export.md
 M feature-specs/system/C-SYSTEM-018-file-resource-lock-diagnosis-and-cleanup.md
 M feature-specs/update/C-UPDATE-001-windows-update-state-pending-work-and-restart.md
 M feature-specs/update/C-UPDATE-002-update-failure-diagnostics-and-troubleshooter.md
 M feature-specs/update/C-UPDATE-003-update-servicing-eligibility-and-readiness.md
 M feature-specs/update/C-UPDATE-004-update-scheduling-pause-and-restart-ux.md
 M feature-specs/update/C-UPDATE-005-delivery-optimization-and-update-bandwidth.md
 M feature-specs/update/C-UPDATE-006-update-storage-requirements.md
 M feature-specs/update/C-UPDATE-007-driver-updates-through-windows-update.md
 M feature-specs/update/C-UPDATE-008-update-rollback-and-regression-correlation.md
 M feature-specs/update/C-UPDATE-009-windows-lifecycle-and-support-status.md
 M feature-specs/update/C-UPDATE-010-update-source-catalog-and-managed-policy-awareness.md
 M feature-specs/usb/C-USB-001-usb-controller-topology-and-device-inventory.md
 M feature-specs/usb/C-USB-002-usb-speed-generation-and-bandwidth-diagnostics.md
 M feature-specs/usb/C-USB-003-usb-device-health-enumeration-and-disconnect-diagnostics.md
 M feature-specs/usb/C-USB-004-usb-power-suspend-sleep-and-wake-diagnostics.md
 M feature-specs/usb/C-USB-005-hid-mouse-keyboard-controller-and-input-diagnostics.md
 M feature-specs/usb/C-USB-006-peripheral-driver-firmware-and-software-conflict-diagnostics.md
 M feature-specs/usb/C-USB-007-usb-c-usb4-thunderbolt-and-docking-diagnostics.md
 M feature-specs/usb/C-USB-008-external-storage-and-surprise-removal-awareness.md
 M feature-specs/usb/C-USB-009-usb-audio-webcam-and-capture-device-awareness.md
 M feature-specs/usb/C-USB-010-bluetooth-adapter-device-and-coexistence-diagnostics.md
 M feature-specs/usb/C-USB-011-ghost-duplicate-and-resource-conflict-diagnostics.md
 M feature-specs/usb/C-USB-012-peripheral-restart-repair-and-outcome-validation.md
 M feature-specs/usb/C-USB-013-usb-peripheral-history-trace-and-root-cause-diagnostics.md
 M feature-specs/usb/C-USB-015-external-peripheral-security-safeguards.md
 M prototypes/system/C-SYSTEM-017/README.md
 M prototypes/system/C-SYSTEM-017/results/PHASE-2-RETURN-PACKAGE.md
 M prototypes/system/C-SYSTEM-017/results/domain-evidence.json
 M prototypes/system/C-SYSTEM-017/scripts/collect-domain-evidence.ps1
 M prototypes/system/C-SYSTEM-017/scripts/generate-return-package.ps1
 M prototypes/system/C-SYSTEM-017/tests/collect-domain-evidence.tests.ps1
?? .gitignore
?? PHASE-2-PROOF-MATRIX.md
?? PHASE-2-RETURN-PACKAGE.md
?? docs/02A-PROTOTYPING-TECHNICAL-PROOF.md
?? prototypes/audio/
?? prototypes/cleaning/
?? prototypes/cpu/
?? prototypes/display/
?? prototypes/drivers/
?? prototypes/energy/
?? prototypes/gpu/
?? prototypes/hardware/
?? prototypes/network/
?? prototypes/privacy/
?? prototypes/processes/
?? prototypes/ram/
?? prototypes/repair/
?? prototypes/security/
?? prototypes/startup/
?? prototypes/storage/
?? prototypes/system/C-SYSTEM-001/results/native-environment.json
?? prototypes/system/C-SYSTEM-001/src/
?? prototypes/system/C-SYSTEM-001/tests/native-environment.tests.ps1
?? prototypes/system/C-SYSTEM-007/
?? prototypes/system/C-SYSTEM-011/
?? prototypes/system/C-SYSTEM-012/
?? prototypes/system/C-SYSTEM-017/results/PHASE-2-PROOF-MATRIX.md
?? prototypes/system/C-SYSTEM-017/results/domain-evidence-export.json
?? prototypes/system/C-SYSTEM-017/results/domain-evidence-export.manifest.json
?? prototypes/system/C-SYSTEM-017/results/final-verification.json
?? prototypes/system/C-SYSTEM-017/results/specified-audit-overrides.json
?? prototypes/system/C-SYSTEM-017/scripts/apply-final-specified-audit.ps1
?? prototypes/system/C-SYSTEM-017/scripts/export-sanitized-evidence.ps1
?? prototypes/system/C-SYSTEM-017/scripts/verify-phase2-campaign.ps1
?? prototypes/system/C-SYSTEM-017/tests/apply-final-specified-audit.tests.ps1
?? prototypes/system/C-SYSTEM-017/tests/export-evidence.tests.ps1
?? prototypes/system/C-SYSTEM-017/tests/generate-return-package.tests.ps1
?? prototypes/system/C-SYSTEM-018/
?? prototypes/update/
?? prototypes/usb/
```

### git log --oneline -5 capturado

```text
74177eb docs(proof): finalize phase 2 return package
6eab645 fix(proof): normalize evidence artifacts
8885513 docs(proof): refresh phase 2 return package
323d493 docs(feature-specs): record phase 2 campaign evidence
a340e30 test(system): add phase 2 evidence prototypes
```

## E. Inventário final de status

- RESEARCH: 73 — C-AUDIO-004, C-AUDIO-005, C-CLEANING-004, C-CLEANING-008, C-CPU-010, C-DISPLAY-003, C-DISPLAY-004, C-DISPLAY-012, C-DISPLAY-013, C-DRIVERS-004, C-ENERGY-001, C-ENERGY-003, C-ENERGY-005, C-ENERGY-006, C-ENERGY-007, C-ENERGY-010, C-ENERGY-012, C-GAMING-002, C-GAMING-004, C-GAMING-005, C-GAMING-006, C-GAMING-007, C-GAMING-008, C-GAMING-012, C-GPU-004, C-GPU-005, C-GPU-007, C-GPU-008, C-GPU-009, C-GPU-010, C-GPU-012, C-GPU-014, C-GPU-015, C-HARDWARE-002, C-HARDWARE-005, C-HARDWARE-006, C-HARDWARE-007, C-NETWORK-004, C-NETWORK-008, C-NETWORK-009, C-NETWORK-010, C-NETWORK-011, C-NETWORK-012, C-NETWORK-014, C-NETWORK-016, C-PRIVACY-002, C-PRIVACY-003, C-PRIVACY-004, C-PRIVACY-005, C-PROCESSES-003, C-RAM-007, C-RAM-008, C-REPAIR-003, C-SECURITY-002, C-SECURITY-004, C-SECURITY-005, C-SECURITY-006, C-STARTUP-004, C-STORAGE-007, C-STORAGE-008, C-STORAGE-012, C-STORAGE-016, C-STORAGE-018, C-STORAGE-019, C-SYSTEM-002, C-SYSTEM-006, C-SYSTEM-008, C-UPDATE-002, C-UPDATE-004, C-UPDATE-005, C-UPDATE-007, C-UPDATE-009, C-USB-004
- SPECIFIED: 90 — C-AUDIO-001, C-AUDIO-003, C-AUDIO-006, C-AUDIO-007, C-AUDIO-010, C-CLEANING-001, C-CLEANING-002, C-CLEANING-005, C-CLEANING-006, C-CLEANING-007, C-CLEANING-009, C-CPU-001, C-CPU-002, C-CPU-003, C-CPU-004, C-CPU-011, C-CPU-015, C-DISPLAY-001, C-DISPLAY-002, C-DRIVERS-001, C-DRIVERS-002, C-DRIVERS-005, C-DRIVERS-007, C-DRIVERS-008, C-ENERGY-013, C-GPU-001, C-GPU-002, C-GPU-006, C-HARDWARE-001, C-HARDWARE-004, C-HARDWARE-009, C-NETWORK-001, C-NETWORK-002, C-NETWORK-003, C-NETWORK-005, C-NETWORK-007, C-NETWORK-013, C-PRIVACY-001, C-PRIVACY-006, C-PROCESSES-001, C-PROCESSES-002, C-PROCESSES-004, C-PROCESSES-005, C-PROCESSES-006, C-PROCESSES-007, C-RAM-001, C-RAM-004, C-RAM-006, C-RAM-009, C-RAM-010, C-RAM-012, C-RAM-014, C-RAM-016, C-RAM-017, C-REPAIR-006, C-REPAIR-008, C-REPAIR-009, C-REPAIR-010, C-SECURITY-001, C-SECURITY-003, C-SECURITY-007, C-STARTUP-001, C-STARTUP-003, C-STARTUP-005, C-STARTUP-006, C-STARTUP-007, C-STARTUP-008, C-STORAGE-001, C-STORAGE-004, C-STORAGE-006, C-STORAGE-009, C-STORAGE-011, C-STORAGE-020, C-SYSTEM-001, C-SYSTEM-003, C-SYSTEM-007, C-SYSTEM-011, C-SYSTEM-015, C-SYSTEM-016, C-SYSTEM-017, C-SYSTEM-018, C-UPDATE-001, C-UPDATE-003, C-UPDATE-006, C-UPDATE-008, C-UPDATE-010, C-USB-001, C-USB-005, C-USB-010, C-USB-015
- PROTOTYPING: 0
- PROVEN: 0
- APPROVED: 0
- BLOCKED: 55 — C-CPU-005, C-CPU-006, C-CPU-007, C-CPU-009, C-CPU-013, C-CPU-014, C-DISPLAY-005, C-DISPLAY-006, C-DISPLAY-007, C-DISPLAY-009, C-DRIVERS-003, C-DRIVERS-006, C-DRIVERS-009, C-ENERGY-004, C-ENERGY-011, C-GAMING-001, C-GAMING-003, C-GAMING-011, C-GAMING-014, C-GPU-003, C-HARDWARE-003, C-HARDWARE-008, C-NETWORK-006, C-RAM-002, C-RAM-003, C-RAM-005, C-RAM-013, C-RAM-015, C-REPAIR-001, C-REPAIR-002, C-REPAIR-004, C-REPAIR-005, C-REPAIR-007, C-SECURITY-008, C-SECURITY-010, C-STARTUP-002, C-STORAGE-002, C-STORAGE-003, C-STORAGE-005, C-STORAGE-013, C-STORAGE-014, C-STORAGE-015, C-STORAGE-021, C-SYSTEM-012, C-SYSTEM-013, C-SYSTEM-014, C-USB-002, C-USB-003, C-USB-006, C-USB-007, C-USB-008, C-USB-009, C-USB-011, C-USB-012, C-USB-013
- DEFERRED: 16 — C-AUDIO-002, C-AUDIO-008, C-AUDIO-009, C-AUDIO-011, C-AUDIO-013, C-CLEANING-003, C-DISPLAY-008, C-DISPLAY-011, C-ENERGY-009, C-GAMING-009, C-GAMING-013, C-GPU-011, C-GPU-013, C-RAM-011, C-STORAGE-010, C-STORAGE-017
- REJECTED: 0

## F. Matriz do ambiente realmente testado

- Windows: Microsoft Windows 11 Pro, versão 10.0.26200, build 26200
- Arquitetura: 64 bits
- CPU: AMD Ryzen 7 5700
- GPU/driver: Radeon RX 570 Series / 31.0.21925.1001
- Motherboard: ASUSTeK COMPUTER INC. PRIME B450M-GAMING/BR Rev X.0x
- Form factor: Desktop
- Admin: False
- Bateria presente: False
- Hypervisor presente: False
- VM: não usada
- Chipset: não inferido; fonte autoritativa não foi provada nesta campanha.

## G. Matriz de prova por Feature ID

| Feature ID | Status | DETECT | PLAN | DRY-RUN | APPLY | VERIFY | SNAPSHOT | ROLLBACK | VERIFY_ROLLBACK | MEASURE | SAFE_PROOF | REBOOT_TEST |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| C-AUDIO-001 | SPECIFIED | PASS (Core Audio endpoint identity hashes, flows, MMDevice states, property availability and default roles) | PASS (read-only; Changes = []) | PASS (no mutation surface invoked) | N/A | PASS (59 endpoints; 6 default flow/role mappings) | NOT_TESTED | N/A | N/A | N/A | PASS | N/A |
| C-AUDIO-002 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-AUDIO-003 | SPECIFIED | PASS (MMDevice active/disabled/unplugged endpoint-state slice) | PASS (read-only; Changes = []) | PASS (no mutation surface invoked) | N/A | PASS (state-mask normalization) | NOT_TESTED | N/A | N/A | N/A | PASS | N/A |
| C-AUDIO-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-AUDIO-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-AUDIO-006 | SPECIFIED | PARTIAL (Core Audio endpoint identity plus PnP AudioEndpoint/Media transport prefixes were collected; exact endpoint-to-parent path remains NOT_TESTED) | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-AUDIO-007 | SPECIFIED | PASS (capture endpoint presence/state/identity slice only) | NOT_TESTED | PASS (no capture stream or mutation) | N/A | PASS (20 capture endpoints enumerated) | NOT_TESTED | N/A | N/A | N/A | PASS + BLOCKED | N/A |
| C-AUDIO-008 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-AUDIO-009 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-AUDIO-010 | SPECIFIED | PARTIAL (Core Audio identity and PnP transport prefixes collected; virtual/physical and capture-path classification intentionally not inferred) | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-AUDIO-011 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-AUDIO-013 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-CLEANING-001 | SPECIFIED | PASS (isolated fixture: provenance, age, ownership class, exclusions and byte accounting) | PASS (isolated fixture only) | PASS (tree fingerprint unchanged) | N/A for this read-only feature | PASS (isolated fixture only) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-CLEANING-002 | SPECIFIED | PASS (isolated fixture) | PASS (2 allowlisted files; 40 bytes) | PASS (tree fingerprint unchanged) | PASS (prototype-owned temp sandbox only) | PASS (eligible files removed; exclusions and bytes verified) | NOT_TESTED | N/A (deletion is not reversible) | N/A | N/A | PASS | N/A |
| C-CLEANING-003 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-CLEANING-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-CLEANING-005 | SPECIFIED | PASS (synthetic diagnostic-artifact fixture) | PASS (expired dump eligible; active/locked dump excluded) | PASS | PASS (prototype-owned fixture only) | PASS (retention and lock boundary) | NOT_TESTED | N/A (deletion is not reversible) | N/A | N/A | PASS | N/A |
| C-CLEANING-006 | SPECIFIED | PASS (protected/unknown/locked/reparse fixture cases) | PASS (default-deny exclusions) | PASS (zero mutation) | PASS (only authorized fixture paths) | PASS (outside sentinel intact) | NOT_TESTED | N/A (deletion is not reversible) | N/A | N/A | PASS | N/A |
| C-CLEANING-007 | SPECIFIED | PASS (isolated fixture) | PASS (same allowlisted candidates consumed by Apply) | PASS (before/after fingerprint identical) | PASS (prototype-owned temp sandbox only) | PASS (count, bytes, retained exclusions and external sentinel) | N/A | N/A (deletion is not reversible) | N/A | N/A | PASS | N/A |
| C-CLEANING-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-CLEANING-009 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-CPU-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-CPU-002 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-CPU-003 | SPECIFIED | PASS | N/A | N/A | N/A | PASS | N/A | N/A | N/A | PASS | PASS + BLOCKED_ADMIN | N/A |
| C-CPU-004 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-CPU-005 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-006 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-007 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-009 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-010 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-011 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-CPU-013 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-014 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-CPU-015 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-DISPLAY-001 | SPECIFIED | PASS (native active-path count, display identity and current mode slice) | NOT_TESTED | N/A for inventory | N/A | PASS (structured constraints in native result) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-DISPLAY-002 | SPECIFIED | PASS (current and supported mode enumeration) | NOT_TESTED | PASS (`CDS_TEST`; no mode Apply) | NOT_TESTED (intentionally not executed on live displays) | PASS (current modes unchanged after valid and invalid validation) | NOT_TESTED | NOT_TESTED | N/A | N/A | PASS | N/A |
| C-DISPLAY-003 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-005 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-DISPLAY-006 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-007 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-DISPLAY-008 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-DISPLAY-009 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-011 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DISPLAY-013 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-DRIVERS-001 | SPECIFIED | PASS (CIM/PnP/CLI metadata slice: 191 signed records; 190 present devices) | PASS (read-only; Changes = []) | PASS (no executable action) | N/A | PASS (188 records with INF/provider/version) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-DRIVERS-002 | SPECIFIED | PASS (healthy present-device baseline: 190 CM_PROB_NONE) | PASS (read-only; Changes = []) | PASS (no device action) | N/A | PASS (healthy count and provenance) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-DRIVERS-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-DRIVERS-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DRIVERS-005 | SPECIFIED | PASS (present devices in conservative critical-class set) | PASS (100 current critical devices classified BLOCK) | PASS (no executable action; no /force) | NOT_TESTED / intentionally prohibited | PASS (all planned critical actions blocked) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-DRIVERS-006 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-DRIVERS-007 | SPECIFIED | PASS (SetupAPI log metadata + 30 PnP/driver Event Log records) | N/A | N/A | N/A | PASS (timestamp/provenance bundle; causality explicitly false) | N/A | N/A | N/A | N/A | PASS | N/A |
| C-DRIVERS-008 | SPECIFIED | PASS (current metadata + SetupAPI/Event Log manifest) | N/A | PASS (manifest-only; no driver action) | N/A | PASS (source hash/timestamps persisted) | N/A | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-DRIVERS-009 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-ENERGY-001 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-ENERGY-003 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-ENERGY-004 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-ENERGY-006 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-ENERGY-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-009 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-010 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-011 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-ENERGY-013 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-GAMING-001 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-GAMING-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GAMING-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-GAMING-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GAMING-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GAMING-006 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GAMING-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GAMING-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GAMING-009 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GAMING-011 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-GAMING-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GAMING-013 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-GAMING-014 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-GPU-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-GPU-002 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-GPU-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-GPU-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-009 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GPU-010 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-011 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-GPU-013 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-014 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-GPU-015 | RESEARCH | PASS | N/A | N/A | N/A | PASS | NOT_TESTED | N/A | N/A | NOT_TESTED | PASS (AMD only) | N/A |
| C-HARDWARE-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-HARDWARE-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-HARDWARE-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-HARDWARE-004 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-HARDWARE-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-HARDWARE-006 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-HARDWARE-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-HARDWARE-008 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-HARDWARE-009 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-002 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-003 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-NETWORK-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-005 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-006 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-NETWORK-007 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-009 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-010 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-NETWORK-011 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-013 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-NETWORK-014 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-NETWORK-016 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-PRIVACY-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-PRIVACY-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-PRIVACY-003 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-PRIVACY-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-PRIVACY-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-PRIVACY-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-PROCESSES-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-PROCESSES-002 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-PROCESSES-003 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-PROCESSES-004 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-PROCESSES-005 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-PROCESSES-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-PROCESSES-007 | SPECIFIED | PASS | PASS | PASS | PASS | PASS | PASS | PASS | PASS | N/A | PASS | N/A |
| C-RAM-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-RAM-002 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-RAM-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-RAM-004 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-RAM-005 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-RAM-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-RAM-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-RAM-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-RAM-009 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-RAM-010 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-RAM-011 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-RAM-012 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-RAM-013 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-RAM-014 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-RAM-015 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | N/A |
| C-RAM-016 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PARTIAL | N/A |
| C-RAM-017 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-REPAIR-001 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-002 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-003 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-004 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-005 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-REPAIR-007 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-REPAIR-008 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-REPAIR-009 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-REPAIR-010 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-SECURITY-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-SECURITY-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SECURITY-003 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-SECURITY-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SECURITY-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SECURITY-006 | RESEARCH | PASS | N/A | N/A | N/A | PASS | NOT_TESTED | N/A | N/A | N/A | PASS / BLOCKED_ADMIN | N/A |
| C-SECURITY-007 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PARTIAL | N/A |
| C-SECURITY-008 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SECURITY-010 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-STARTUP-001 | SPECIFIED | PASS | N/A | N/A | N/A | PASS | N/A | N/A | N/A | N/A | PASS | N/A |
| C-STARTUP-002 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | NOT_TESTED | BLOCKED |
| C-STARTUP-003 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-STARTUP-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STARTUP-005 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PASS | N/A |
| C-STARTUP-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-STARTUP-007 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-STARTUP-008 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-STORAGE-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-STORAGE-002 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | FAIL | N/A |
| C-STORAGE-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | FAIL | N/A |
| C-STORAGE-004 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-STORAGE-005 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | FAIL | BLOCKED |
| C-STORAGE-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-STORAGE-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-STORAGE-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-009 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-STORAGE-010 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-011 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | PASS | N/A |
| C-STORAGE-012 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-013 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-STORAGE-014 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-015 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-STORAGE-016 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-017 | DEFERRED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-018 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-STORAGE-019 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-STORAGE-020 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-STORAGE-021 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SYSTEM-001 | SPECIFIED | PASS | N/A | N/A | N/A | PASS | N/A | N/A | N/A | N/A | PASS | N/A |
| C-SYSTEM-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-SYSTEM-003 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-SYSTEM-006 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SYSTEM-007 | SPECIFIED | PASS | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A | N/A | N/A | N/A | PASS | N/A |
| C-SYSTEM-008 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-SYSTEM-011 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-SYSTEM-012 | BLOCKED | PASS | N/A | N/A | N/A | FAIL | N/A | N/A | N/A | NOT_TESTED | FAIL / BLOCKED_ADMIN | N/A |
| C-SYSTEM-013 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-SYSTEM-014 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-SYSTEM-015 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | PARTIAL | N/A |
| C-SYSTEM-016 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-SYSTEM-017 | SPECIFIED | PASS | N/A | N/A | N/A | PASS | N/A | N/A | N/A | N/A | PASS | N/A |
| C-SYSTEM-018 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-UPDATE-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-UPDATE-002 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-UPDATE-003 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |
| C-UPDATE-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-UPDATE-005 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A |
| C-UPDATE-006 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-UPDATE-007 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-UPDATE-008 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-UPDATE-009 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-UPDATE-010 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PARTIAL | N/A |
| C-USB-001 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-USB-002 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | NOT_TESTED | UNSUPPORTED | N/A |
| C-USB-003 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-USB-004 | RESEARCH | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-USB-005 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | PASS | N/A |
| C-USB-006 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-USB-007 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-USB-008 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-USB-009 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-USB-010 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PARTIAL | N/A |
| C-USB-011 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-USB-012 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A |
| C-USB-013 | BLOCKED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | NOT_TESTED | N/A |
| C-USB-015 | SPECIFIED | NOT_TESTED | NOT_TESTED | NOT_TESTED | N/A | NOT_TESTED | N/A | N/A | N/A | N/A | PASS | N/A |

Cobertura explícita de prova segura entre specs `SPECIFIED`: 90/90. A coluna `SAFE_PROOF` registra a fatia adicional executada sem reescrever os stages ainda não exercitados como `PASS`.

## H. Falhas encontradas

- A premissa de que o check read-only do component store executaria sem elevação foi refutada neste host: DISM retornou 740.
- A premissa de que `\Processor(_Total)\% Processor Time` seria portável foi refutada neste host localizado: `Get-Counter` retornou "O objeto especificado não foi encontrado"; a prova precisou descobrir `\Processador(_Total)\% tempo de processador`.
- A captura ETW/WPR real não pôde iniciar em sessão não elevada: `wpr -start GeneralProfile -filemode` retornou exit code -984068079 / erro 0xc5585011.
- `C-STORAGE-002` e `C-STORAGE-003`: o provider de reliability/temperature retornou acesso CIM indisponível; nenhum dado foi inventado.
- `C-STORAGE-005`: a tentativa read-only de `chkdsk /scan` falhou com acesso negado; nenhum modo de repair foi invocado.
- `C-SECURITY-003`: Secure Boot e TPM retornaram acesso negado/estado não estruturado sem elevação; VBS permaneceu como sinal parcial.
- `C-ENERGY-013`: `powercfg /requests` e `/energy` retornaram exit code 1 por exigirem administrador; o estado ficou PARTIAL/UNSUPPORTED.
- O inventário amplo via fontes genéricas (CIM/PnP/cmdlets) mostrou-se apenas evidência parcial para várias specs; não substitui APIs mais próximas do subsistema.
- Revisão cruzada encontrou contratos herdados de Apply/Rollback em features declaradas read-only; foram corrigidos em C-CPU-011, C-DRIVERS-003 e C-DRIVERS-009.
- A inconsistência C-SYSTEM-007/C-SYSTEM-017 foi corrigida: C-SYSTEM-017 continua sendo o bundle compartilhado de coleta/export e não é prova de C-SYSTEM-007; C-SYSTEM-007 agora possui prototype próprio de baseline/diff Registry.

## I. Features que mudaram de decisão

- Nenhuma mudança de status contra o commit-base.
- Nenhuma promoção para PROVEN/APPROVED foi feita; as provas parciais não satisfazem esses gates.
- Auditoria final adicionou provas específicas sem promoção indevida: C-SYSTEM-001 (P/Invoke), C-SYSTEM-007 (Registry/snapshot/rollback), C-CPU-003 (Performance Counters), C-SYSTEM-012 (ETW/WPR FAIL preservado), C-GPU-015 (ADL entrypoints), C-SECURITY-006 (admin/UAC), C-PROCESSES-007 (restart de processo controlado).

## J. Questões ainda abertas que impedem PROVEN/APPROVED

| Feature ID | Status | Primeira questão/gate aberto preservado na spec |
|---|---|---|
| C-AUDIO-001 | SPECIFIED | Inventário read-only via Core Audio (`IMMDeviceEnumerator`/endpoint properties) e PnP para contexto de dispositivo. |
| C-AUDIO-002 | DEFERRED | Enumerar sessões via `IAudioSessionManager2`/`IAudioSessionEnumerator`; volume/mute por sessão usa interfaces públicas quando a ação for explicitamente solicitada. |
| C-AUDIO-003 | SPECIFIED | Diagnóstico via PnP problem status + endpoint state + eventos; ausência de endpoint não identifica sozinho falha de driver. |
| C-AUDIO-004 | RESEARCH | Core Audio permite ler capabilities/formatos em várias superfícies, mas alterar o “Default Format” e enhancements/APO state do Windows não possui um único contrato público estável para todos os endpoints/drivers. |
| C-AUDIO-005 | RESEARCH | Spatial sound, channel layout e speaker configuration possuem APIs/capabilities diferentes; detecção não garante mutação genérica suportada. |
| C-AUDIO-006 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-AUDIO-007 | SPECIFIED | Detect combina capture endpoint availability, mute/volume quando suportado, permission/privacy state e captura controlada de nível/sinal. |
| C-AUDIO-008 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-AUDIO-009 | DEFERRED | Orquestrador de reparo: pode reiniciar/re-scan dispositivo via caminho PnP suportado e encaminhar driver repair; não reconfigura formatos/APOs por heurística. |
| C-AUDIO-010 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-AUDIO-011 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-AUDIO-013 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CLEANING-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CLEANING-002 | SPECIFIED | Cleanup só atua em categorias allowlisted cuja propriedade/rebuildability está estabelecida; preview lista paths/categorias/bytes antes de excluir. |
| C-CLEANING-003 | DEFERRED | Discovery/review é read-only; Downloads, arquivos grandes/antigos/duplicados e cloud dehydration são recomendações, nunca exclusão automática. |
| C-CLEANING-004 | RESEARCH | “Resíduo” não pode ser determinado apenas porque arquivo/pasta não aparece no uninstall registry; apps podem manter dados compartilhados, perfis, plugins e licenças. |
| C-CLEANING-005 | SPECIFIED | Inventariar logs/dumps conhecidos com idade/tamanho/provenance; não apagar artefatos recentes ligados a crash/repair em andamento sem aviso. |
| C-CLEANING-006 | SPECIFIED | Capability de gate: canonicalize path, detect reparse/hardlink/sparse context, ownership, active-use e protected locations antes de qualquer cleaner. |
| C-CLEANING-007 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CLEANING-008 | RESEARCH | Storage Sense possui Settings e políticas CSP/GPO documentadas, mas Policy CSP é instrumento de gerenciamento e não deve ser usado como substituto silencioso da preferência local do usuário. |
| C-CLEANING-009 | SPECIFIED | Read-only: enumerar volumes/mount points, filesystem, capacidade/free space e mapear cleanup candidate ao volume real. |
| C-CPU-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CPU-002 | SPECIFIED | Metodologia definida para protótipo: single-thread e multi-thread workloads determinísticos, warm-up, cooldown/thermal-state check, repetição e distribuição de resultados; registrar CPU topology, power mode e background load. |
| C-CPU-003 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CPU-004 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CPU-005 | BLOCKED | Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec? |
| C-CPU-006 | BLOCKED | Capability diagnóstica com camadas: dados genéricos do OS/perf quando disponíveis + adaptadores oficiais AMD/Intel/vendor opcionais para temperatura, package power e limiter reasons. |
| C-CPU-007 | BLOCKED | Diagnóstico usa APIs públicas de processor groups/CPU sets/affinity/priority e ETW quando necessário; mutations genéricas de process priority/affinity pertencem a `C-PROCESSES-005`. |
| C-CPU-009 | BLOCKED | PMU é capability avançada e optional: enumerar/usar somente counters expostos por mecanismo suportado no hardware/OS; eventos são arquitetura/model-specific. |
| C-CPU-010 | RESEARCH | Hypervisor/security mitigation context possui sinais OS suportados, mas “microcode version/update recommendation” não tem uma fonte universal e estável cross-vendor definida nesta spec. |
| C-CPU-011 | SPECIFIED | Safety gate documental: bloquear “use all cores” boot tweaks, scheduler Registry packs, forced realtime/high priority e affinity myths quando não há workload-specific evidence. |
| C-CPU-013 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CPU-014 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-CPU-015 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DISPLAY-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DISPLAY-002 | SPECIFIED | CCD (`QueryDisplayConfig`/`SetDisplayConfig`) define topologia/modos e `SDC_VALIDATE` permite validar uma configuração sem persistir alteração. |
| C-DISPLAY-003 | RESEARCH | Detecção de Advanced Color/HDR é possível em versões modernas, porém toggle/configuração e semântica de SDR/HDR variam por build, GPU/driver e display. |
| C-DISPLAY-004 | RESEARCH | VRR efetivo depende de OS, driver, display, mode e aplicativo; capability flag não prova que a apresentação está usando VRR. |
| C-DISPLAY-005 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DISPLAY-006 | BLOCKED | Diagnóstico deve separar DPI/scaling por monitor, resolution mode e color profile; essas dimensões não são equivalentes. |
| C-DISPLAY-007 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DISPLAY-008 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DISPLAY-009 | BLOCKED | Ownership corrigido: esta capability diagnostica brilho/display-off/wake behavior e consome o estado de energia; mutações de timeout/power plan pertencem ao domínio Energy. |
| C-DISPLAY-011 | DEFERRED | Shared safeguard para operações de modo: `SDC_VALIDATE` antes de Apply quando aplicável, snapshot CCD, timeout de confirmação na UX futura e rollback para topologia/modos originais. |
| C-DISPLAY-012 | RESEARCH | DDC/CI/MCCS e vendor utilities não são universais; nem todo monitor/connection expõe controle ou firmware update. |
| C-DISPLAY-013 | RESEARCH | Virtual/remote/indirect displays podem alterar topologia e capabilities; critérios robustos de classificação cross-vendor ainda precisam de prova. |
| C-DRIVERS-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DRIVERS-002 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DRIVERS-003 | BLOCKED | Capability de awareness/recommendation: inventariar provider/version/date/INF e distinguir Windows Update, OEM/vendor e driver store quando observável. |
| C-DRIVERS-004 | RESEARCH | PnPUtil documenta add/install/delete/export/restart, mas isso não prova um contrato determinístico para restaurar “o driver anterior” equivalente ao Device Manager Roll Back Driver. |
| C-DRIVERS-005 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DRIVERS-006 | BLOCKED | Driver Store inventory/export/delete só usa PnPUtil suportado; remoção é ação explícita e nunca “limpeza automática”. |
| C-DRIVERS-007 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DRIVERS-008 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-DRIVERS-009 | BLOCKED | Read-only: identificar policy/device-install restrictions quando observável e devolver `POLICY_BLOCKED/MANAGED`; não contornar GPO/MDM. |
| C-ENERGY-001 | RESEARCH | Power scheme enumeration/activation é documentada via Power APIs/powercfg, mas Windows modern standby/power mode overlays precisam ser separados de scheme clássico. |
| C-ENERGY-003 | RESEARCH | Microsoft recomenda que PPM customization siga orientação do silicon vendor; não há preset universal seguro. |
| C-ENERGY-004 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-ENERGY-005 | RESEARCH | Sleep/hibernate/wake diagnostics é suportável via powercfg, mas Fast Startup/hiberfile/power state transitions têm dependências e impacto que exigem Apply/rollback próprios. |
| C-ENERGY-006 | RESEARCH | Device power management é class/vendor-specific; capability genérica não deve escrever “Allow computer to turn off” Registry/device properties sem contrato público. |
| C-ENERGY-007 | RESEARCH | Display timeout é power-plan owned; adaptive brightness/DRR/brightness capabilities variam por display/platform. |
| C-ENERGY-009 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-ENERGY-010 | RESEARCH | Energy attribution pode usar OS telemetry, mas per-process efficiency/power throttling policy deve usar APIs públicas e respeitar process identity. |
| C-ENERGY-011 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-ENERGY-012 | RESEARCH | Windows 11 Energy Saver e versões anteriores Battery Saver não devem ser tratados como o mesmo contrato em todas as builds. |
| C-ENERGY-013 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GAMING-001 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GAMING-002 | RESEARCH | Game Mode/Game Bar/capture settings possuem superfícies/versionamento distintos; não assumir Registry interno como contrato. Definir APIs/policy e effective state por build; sem isso manter diagnóstico com Unknown. |
| C-GAMING-003 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GAMING-004 | RESEARCH | Refresh/mode pode vir de Display, mas VRR/frame cap effective dependem OS/driver/app. Separar configured capability de observed presentation; não aplicar cap/VRR universal. |
| C-GAMING-005 | RESEARCH | DLSS/FSR/XeSS/frame generation/Reflex/Anti-Lag são vendor/game-specific. Detectar suporte/estado só por integração/evidência oficial; não oferecer toggle genérico. |
| C-GAMING-006 | RESEARCH | Telemetry de frames pode ser coletada, mas classificação de stutter precisa thresholds/workload methodology e provenance. Definir sampling, process matching e invalidation antes de SPECIFIED. |
| C-GAMING-007 | RESEARCH | Crash/hang/dependency diagnosis agrega fontes diferentes; reparos precisam ownership separado. Definir supported evidence and correlation sem “reinstall everything” workflow. |
| C-GAMING-008 | RESEARCH | Perfil só pode aplicar capabilities já PROVEN e reversíveis; não armazenar tweak packs por jogo. Contrato depende dos owners e conflito com user/vendor settings ainda precisa prova. |
| C-GAMING-009 | DEFERRED | Método de prova definido: capturar frame presentation/frametime por processo, warm-up, janela estável, repetições e distribuição; registrar mode/refresh/power/driver/background state. |
| C-GAMING-011 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GAMING-012 | RESEARCH | Gaming context é derivado de hardware/display/input features; definir criteria sem classificar PC por marketing tier e sem duplicar detectors. Validar heterogeneous/hybrid systems. |
| C-GAMING-013 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GAMING-014 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-002 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-003 | BLOCKED | Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec? |
| C-GPU-004 | RESEARCH | Temperatura/power/limiter reasons exigem vendor API/driver telemetry; definir adapters AMD/Intel/NVIDIA e Unsupported sem inferência. |
| C-GPU-005 | RESEARCH | DRED/TDR/event evidence pode diagnosticar, mas causalidade driver vs hardware/app exige correlação; repair delegated Drivers. |
| C-GPU-006 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-007 | RESEARCH | HAGS/presentation behavior/effective scheduling são distintos; não usar Registry internal toggle nem inferir performance benefit. |
| C-GPU-008 | RESEARCH | Windows preference/hybrid routing pode ser vendor/OS managed; API pública persistente para consumer Apply precisa prova e rollback. |
| C-GPU-009 | RESEARCH | PCI capability/readiness não basta para provar ReBAR ativo/utilizado cross-vendor. Definir truth source AMD/NVIDIA/Intel and unsupported. |
| C-GPU-010 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-011 | DEFERRED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-GPU-012 | RESEARCH | Shader caches são driver/app-owned; limpeza não é otimização universal e pode causar recompilation stutter. Definir provenance before delete. |
| C-GPU-013 | DEFERRED | Benchmark deve separar graphics/compute workloads, warm-up, shader compilation, repeated runs e monitorar clocks/thermal/power context sem alterar tuning. |
| C-GPU-014 | RESEARCH | Tuning é vendor-specific e pode operar fora de specification; validation cannot authorize unsafe clocks/voltage. Need vendor adapters and rollback. |
| C-GPU-015 | RESEARCH | Vendor capability layer needs official SDK versioning/licensing/feature mapping and fallback; no private driver interfaces. |
| C-HARDWARE-001 | SPECIFIED | Inventário base usa SMBIOS via `GetSystemFirmwareTable`, PnP/SetupAPI e APIs específicas de CPU/GPU/storage; vendor utilities são enrichment, não truth única. |
| C-HARDWARE-002 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-HARDWARE-003 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-HARDWARE-004 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-HARDWARE-005 | RESEARCH | Windows não fornece telemetria universal de qualidade das linhas de PSU/VRM. Correlacionar WHEA/thermal/vendor sensors sem afirmar PSU fault por inferência única; definir confidence model. |
| C-HARDWARE-006 | RESEARCH | Stress testing pode causar instabilidade/temperatura alta; definir workloads isolados, stop conditions, sensor reliability and informed consent. No final implementation yet. |
| C-HARDWARE-007 | RESEARCH | Bottleneck/fault classification é inferência multi-signal; precisa labeled scenarios/benchmarks, confidence and no single-metric causality. |
| C-HARDWARE-008 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-HARDWARE-009 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-002 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-003 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-004 | RESEARCH | Detect resolver/config/query behavior, but DNS Apply must preserve per-interface/DHCP policy and not auto-switch provider; rollback exact original including automatic state. |
| C-NETWORK-005 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-006 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-007 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-008 | RESEARCH | Do not apply TCP autotuning/congestion/registry gaming tweaks universally. Define supported netsh/API observation and only repair when causal evidence exists. |
| C-NETWORK-009 | RESEARCH | Firewall state/policy can be diagnosed, but rule mutation overlaps Security and corporate policy. Establish owner and never disable firewall as network fix. |
| C-NETWORK-010 | RESEARCH | Bufferbloat requires controlled load + latency baseline and network path context; one speed test is insufficient. Define methodology and remote endpoint variability. |
| C-NETWORK-011 | RESEARCH | VPN/proxy/virtual adapters are app/policy specific. Detect without disabling; repair requires owner/vendor and must preserve routes/DNS. |
| C-NETWORK-012 | RESEARCH | NIC power/EEE/wake mutation is device/vendor and overlaps Energy. Do not disable EEE/power saving universally; define delegated ownership. |
| C-NETWORK-013 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-NETWORK-014 | RESEARCH | Network reset commands have different destructiveness and may lose static IP/VPN/proxy config. Need exact snapshot/reapply validation for every stage; avoid universal winsock/IP reset. |
| C-NETWORK-016 | RESEARCH | WWAN/hotspot capabilities depend on hardware/carrier/build. Keep diagnostic until public API support and policy interactions are tested. |
| C-PRIVACY-001 | SPECIFIED | Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização? |
| C-PRIVACY-002 | RESEARCH | A documentação Microsoft confirma superfícies de **policy/MDM** para diagnostic data, tailored experiences e App Privacy, mas isso não equivale a uma API pública de preferência local destinada a um app consumidor. Antes de `SPECIFIED` para mutação, provar uma superfície suportada de escrita que represente o mesmo estado efetivo que Settings e que não dependa de Registry não documentado. |
| C-PRIVACY-003 | RESEARCH | A Policy CSP de Privacy documenta estados administrados para câmera, microfone e localização, inclusive `User in control`, `Force allow` e `Force deny`; isso deve ser modelado como camada de policy, não confundido com a preferência local do usuário. |
| C-PRIVACY-004 | RESEARCH | As superfícies de activity/search/speech/cloud privacy variam por recurso e versão; não assumir que um único switch/Registry representa o estado efetivo. |
| C-PRIVACY-005 | RESEARCH | O guia de conformidade da Microsoft documenta política para Find My Device (`Experience/AllowFindMyDevice` / ADMX), mas policy gerenciada não é equivalente a uma API pública de toggle local do usuário. |
| C-PRIVACY-006 | SPECIFIED | Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização? |
| C-PROCESSES-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-PROCESSES-002 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-PROCESSES-003 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-PROCESSES-004 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-PROCESSES-005 | SPECIFIED | Execute the Windows prototype/test matrix for priority class, affinity, memory priority and power-throttling on supported Windows 11 builds; documentary definition is sufficient for `SPECIFIED`, not `PROVEN`. |
| C-PROCESSES-006 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-PROCESSES-007 | SPECIFIED | Execute Windows tests for critical-process blocking, access denied/protected targets, ordinary termination, target exit between plan/apply, and Restart Manager `bRestartable` true/false cases before `PROVEN`. |
| C-RAM-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-002 | BLOCKED | Prototype `Win32_PhysicalMemory`/SMBIOS normalization on DDR4 and DDR5 systems and compare configured clock/voltage/module identity with firmware/vendor tooling before `PROVEN`. |
| C-RAM-003 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-004 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-005 | BLOCKED | Prototype automatic-managed → custom → rollback and custom → automatic-managed on Windows 11, including configured-vs-runtime behavior across reboot. |
| C-RAM-006 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-007 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-008 | RESEARCH | Validate the chosen pressure/cache counters on Windows 11 and compare with Task Manager/Performance Monitor before `PROVEN`. |
| C-RAM-009 | SPECIFIED | Build unit fixtures proving that undocumented/no-benefit/no-rollback candidates are rejected rather than silently normalized to defaults. |
| C-RAM-010 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-011 | DEFERRED | Build and execute the isolated benchmark prototype; calibrate buffer sizes/run count so measurement overhead is small and the test is long enough to be stable without becoming a thermal stress test. |
| C-RAM-012 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-013 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-014 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-RAM-015 | BLOCKED | Validate module `ConfiguredVoltage` labeling against SMBIOS/WMI on target systems. |
| C-RAM-016 | SPECIFIED | Execute a prototype that queries `GetLargePageMinimum` and, in a controlled environment with the privilege already granted, confirms a `MEM_LARGE_PAGES` allocation; failure without privilege must be represented explicitly. |
| C-RAM-017 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-REPAIR-001 | BLOCKED | A capacidade é um orquestrador: não inventa mutações próprias; classifica sintomas/evidências e delega para o reparador owner (component store, AppX, PnP, boot etc.). |
| C-REPAIR-002 | BLOCKED | Método suportado definido: diagnosticar component store com DISM (`/CheckHealth`/`/ScanHealth` quando aplicável), reparar com `/RestoreHealth` quando necessário e executar `sfc /scannow` para arquivos protegidos. |
| C-REPAIR-003 | RESEARCH | Continuar em `RESEARCH`: BCDBoot/BCD e WinRE possuem ferramentas oficiais, porém o plano de Apply/rollback precisa ser separado por cenário UEFI/BIOS, layout de partição e estado de BitLocker/Secure Boot. |
| C-REPAIR-004 | BLOCKED | A feature deve diagnosticar operações pendentes/reboot necessário por superfícies suportadas do componente responsável e apresentar a ação suportada (normalmente concluir/reiniciar), não “limpar” marcadores internos. |
| C-REPAIR-005 | BLOCKED | Escopo mutável suportado nesta spec: pacotes MSIX/AppX quando a plataforma expõe operação documentada, como `Reset-AppxPackage`; aplicativos Win32 arbitrários devem retornar `APP_SPECIFIC_REPAIR_REQUIRED`. |
| C-REPAIR-006 | SPECIFIED | Capability de roteamento: cada subsistema só pode executar reparo documentado pelo seu owner (network, audio, update, storage etc.). |
| C-REPAIR-007 | BLOCKED | Reparos genéricos suportados ficam limitados a reenumeração e reinício de dispositivo quando aplicável (`pnputil /scan-devices`, `/restart-device`) e a fluxos de driver pertencentes ao domínio Drivers. |
| C-REPAIR-008 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-REPAIR-009 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-REPAIR-010 | SPECIFIED | Esta é uma capability de gate/diagnóstico: calcula confiança e readiness a partir de evidência disponível, reversibilidade declarada, snapshot verificável, requisitos de reboot e disponibilidade do caminho de recuperação. |
| C-SECURITY-001 | SPECIFIED | Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal? |
| C-SECURITY-002 | RESEARCH | Detect pode usar superfícies documentadas do Windows Security/Defender/Firewall, porém a parte `Configuration` continua sem contrato único e seguro para todos os provedores de endpoint protection. |
| C-SECURITY-003 | SPECIFIED | Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal? |
| C-SECURITY-004 | RESEARCH | Diagnóstico de VBS/Credential Guard/Memory Integrity é possível, mas a mutação tem dependências de hardware, firmware, drivers, boot e política que exigem plano próprio e testes. |
| C-SECURITY-005 | RESEARCH | SmartScreen, Smart App Control e Exploit Protection não devem ser tratados como um único toggle; possuem contratos e ciclos de vida distintos. |
| C-SECURITY-006 | RESEARCH | UAC, BitLocker/Device Encryption e Windows Hello têm riscos, permissões e mecanismos independentes; a agregação serve para diagnóstico, não para um Apply comum. |
| C-SECURITY-007 | SPECIFIED | Capability read-only/recommendation: correlacionar versão/build/edição com lifecycle/support e estado de atualização de segurança disponível; não instala update diretamente. |
| C-SECURITY-008 | BLOCKED | Capability de segurança/read-only: detectar sinais de política/tamper/provider conflict e bloquear recomendações que reduzam proteção sem justificativa explícita. |
| C-SECURITY-010 | BLOCKED | Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal? |
| C-STARTUP-001 | SPECIFIED | Cobertura mínima suportada definida: Run/RunOnce (HKCU/HKLM incluindo views relevantes) e Startup folders; outras extensões (scheduled tasks, services, shell/winlogon) devem ser classificadas pela fonte e nunca achatadas como se fossem equivalentes. |
| C-STARTUP-002 | BLOCKED | A Microsoft documenta impacto de Startup Apps com CPU e disk I/O; essa classificação pode ser exibida quando a fonte está disponível, sem reinterpretar limites como score universal do BeadWise. |
| C-STARTUP-003 | SPECIFIED | Define and version the initial role taxonomy/rule evidence without turning vendor names into unconditional allow/deny lists. |
| C-STARTUP-004 | RESEARCH | Scoring pode ser definido, mas o controle genérico equivalente ao botão Enable/Disable do Task Manager não possui contrato público único para todas as fontes de startup. |
| C-STARTUP-005 | SPECIFIED | Define minimum sample-count/confidence policy for before/after claims after prototype data is available. |
| C-STARTUP-006 | SPECIFIED | Prototype command-line target resolution against quoting/environment/protocol/rundll32 edge cases; ambiguous commands must remain unresolved rather than guessed. |
| C-STARTUP-007 | SPECIFIED | Define the cross-domain contract with Security and System accessibility capability once those specs are completed; until then corresponding guard may return `Unknown/manual only`. |
| C-STARTUP-008 | SPECIFIED | Select durable storage format/encryption/integrity mechanism later at architecture level without changing the snapshot contract defined here. |
| C-STORAGE-001 | SPECIFIED | Como obter PCIe negotiated width/speed de forma confiável em todos os controladores? |
| C-STORAGE-002 | BLOCKED | Política de atributos ATA vendor-specific por fabricante. |
| C-STORAGE-003 | BLOCKED | Quais campos térmicos são expostos por SATA/AHCI sem SMART vendor parsing? |
| C-STORAGE-004 | SPECIFIED | Fonte segura para tamanho “on disk” de placeholders cloud sem hidratação. |
| C-STORAGE-005 | BLOCKED | Mapear exatamente modos CHKDSK/Repair-Volume por NTFS vs ReFS e build. |
| C-STORAGE-006 | SPECIFIED | Como representar end-to-end TRIM confidence sob RAID/USB sem vendor-specific ATA passthrough? |
| C-STORAGE-007 | RESEARCH | Mapear campos exatos de STORAGE_WRITE_CACHE_PROPERTY úteis para UI sem expor detalhes enganosos. |
| C-STORAGE-008 | RESEARCH | Catálogo robusto de event IDs/provider por StorAHCI/StorNVMe/vendor drivers. |
| C-STORAGE-009 | SPECIFIED | Padronização de percentis via ETW vs counters agregados. |
| C-STORAGE-010 | DEFERRED | Perfis finais e parâmetros só podem ser congelados após calibração em HDD/SATA SSD/NVMe de classes diferentes. |
| C-STORAGE-011 | SPECIFIED | Mecanismo oficial programático atual para algumas categorias específicas sem depender de UI/cleanmgr. |
| C-STORAGE-012 | RESEARCH | Escolher VSS COM API final e como identificar snapshots orphan sem conhecer software de backup. |
| C-STORAGE-013 | BLOCKED | Backend final: BitLocker WMI API vs PowerShell dependency. |
| C-STORAGE-014 | BLOCKED | Quais repair cmdlets entram no escopo do produto e com quais safeguards por resiliency type? |
| C-STORAGE-015 | BLOCKED | Forma confiável de detectar uso real de DirectStorage por jogo sem instrumentação do próprio jogo. |
| C-STORAGE-016 | RESEARCH | Quais launchers entram no escopo e APIs suportadas para detectar/mover instalações. |
| C-STORAGE-017 | DEFERRED | Taxonomia global de risk/reason codes deve alinhar com arquitetura fora do domínio. |
| C-STORAGE-018 | RESEARCH | Existe API documentada confiável para estado APST runtime em Windows além de feature/protocol query e driver behavior? |
| C-STORAGE-019 | RESEARCH | Capability read-only: contextualizar SSD/NVMe/SATA, health/endurance somente quando protocolo ou vendor expõe atributo com semântica conhecida. |
| C-STORAGE-020 | SPECIFIED | Manter taxonomia alinhada à política global de ChangePlan. |
| C-STORAGE-021 | BLOCKED | Definir backend final: DISM API vs PowerShell wrapper. |
| C-SYSTEM-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-002 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-003 | SPECIFIED | Capability lógica compartilhada: recomendações devem ser derivadas de estado detectado + regra versionada + evidência + confidence, nunca de “different from default”. |
| C-SYSTEM-006 | RESEARCH | Inventário/health é documentável via Service Control Manager e Task Scheduler, porém Configuration/Repair genérico continua amplo demais. |
| C-SYSTEM-007 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-008 | RESEARCH | DISM fornece inventário e enable/disable de Optional Features, mas “core component health” não deve ser tratado como o mesmo mecanismo. |
| C-SYSTEM-011 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-012 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-013 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-014 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-015 | SPECIFIED | Read-only gate compartilhado: registrar carga de fundo, power state, reboot/pending maintenance, thermal state e condições que invalidem comparabilidade antes de benchmark. |
| C-SYSTEM-016 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-017 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-SYSTEM-018 | SPECIFIED | Diagnóstico de arquivos em uso deve preferir Restart Manager para registrar recursos e identificar aplicações/serviços relacionados quando suportado. |
| C-UPDATE-001 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-UPDATE-002 | RESEARCH | Histórico/erro pode ser diagnosticado, mas o antigo troubleshooter/MSDT não deve ser assumido como mecanismo permanente de automação; fluxos modernos podem ser Get Help/servicing-owned. |
| C-UPDATE-003 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-UPDATE-004 | RESEARCH | Active Hours, pause/deferral e restart UX variam entre consumer settings e políticas de gerenciamento; Policy CSP/GPO não deve ser usado como substituto de preferência local de consumidor. |
| C-UPDATE-005 | RESEARCH | Delivery Optimization possui políticas documentadas, mas aplicar policy localmente em um app consumidor pode conflitar com gerenciamento corporativo e não equivale necessariamente à UI do usuário. |
| C-UPDATE-006 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-UPDATE-007 | RESEARCH | Awareness de driver updates é válida, mas seleção/instalação/rollback pertence ao owner Drivers e ao Windows Update. |
| C-UPDATE-008 | SPECIFIED | Capability diagnóstica: correlacionar timeline de updates com início de regressão sem afirmar causalidade apenas por proximidade temporal. |
| C-UPDATE-009 | RESEARCH | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-UPDATE-010 | SPECIFIED | Read-only: identificar origem/gerenciamento quando observável (Windows Update/Microsoft Update/WSUS/policy) e não sobrescrever configuração administrada. |
| C-USB-001 | SPECIFIED | Read-only topology/inventory com SetupAPI/PnP e IOCTLs USB documentados quando disponíveis; preservar controller/hub/port/device identity e `Unknown` para campos não expostos. |
| C-USB-002 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-USB-003 | BLOCKED | Detect combina PnP problem status, presença/reenumeração e eventos correlacionados; uma desconexão isolada não prova cabo/porta/energia. |
| C-USB-004 | RESEARCH | Diagnóstico de wake/power é válido, porém mutações de selective suspend/power policy se sobrepõem ao domínio Energy; ownership deve ser único. |
| C-USB-005 | SPECIFIED | Capability diagnóstica/read-only por padrão; usar PnP/SetupAPI e APIs específicas documentadas da classe/protocolo, com vendor data apenas como enriquecimento opcional. |
| C-USB-006 | BLOCKED | Capability diagnóstica/read-only por padrão; usar PnP/SetupAPI e APIs específicas documentadas da classe/protocolo, com vendor data apenas como enriquecimento opcional. |
| C-USB-007 | BLOCKED | Capability diagnóstica/read-only por padrão; usar PnP/SetupAPI e APIs específicas documentadas da classe/protocolo, com vendor data apenas como enriquecimento opcional. |
| C-USB-008 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-USB-009 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-USB-010 | SPECIFIED | Capability diagnóstica/read-only por padrão; usar PnP/SetupAPI e APIs específicas documentadas da classe/protocolo, com vendor data apenas como enriquecimento opcional. |
| C-USB-011 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-USB-012 | BLOCKED | Apply genérico suportado fica limitado a restart/re-scan PnP quando dispositivo suporta e usuário autorizou (`pnputil /restart-device`, `/scan-devices`). |
| C-USB-013 | BLOCKED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |
| C-USB-015 | SPECIFIED | Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec? |

## K. Dependências de hardware/ambiente ainda não testadas

- Intel CPU e topologias híbridas não testadas.
- Intel GPU e NVIDIA GPU não testadas; apenas AMD Radeon RX 570 presente.
- Laptop, bateria, Modern Standby e caminhos AC/DC não testados.
- ARM64 não testado.
- VM/hypervisor, RDP e virtual display não testados.
- Dois caminhos de display ativos foram observados; correlação física source/target/EDID, HDR, VRR, DDC/CI e hotplug não foram testados.
- Wi-Fi, WWAN/hotspot, VPN corporativa, proxy gerenciado e policy corporativa não testados.
- BitLocker/device encryption, Storage Spaces/RAID e cenários de filesystem corrompido não testados.
- USB4/Thunderbolt/dock, Bluetooth coexistence e falhas USB induzidas não testados.
- Múltiplos endpoints Core Audio foram enumerados; correlação física/PnP, Bluetooth/USB audio, spatial sound e sleep/resume não foram testados.
- Reboot/logoff e retomada pós-reboot não executados.
- Captura WPR/ETW elevada não executada porque exigiria elevação/UAC fora da sessão atual.

## L. Evidência útil para a Fase 3 (fatos, não decisões)

- Registry declarado: 14 specs.
- PowerShell declarado: 89 specs.
- WMI/CIM declarado: 210 specs.
- Win32 declarado: 221 specs.
- Vendor API declarado: 3 specs.
- Shared Capability declarada: 19 specs.
- Snapshot explicitamente necessário: 103 specs.
- Snapshot explicitamente persistente após reboot: 104 specs.
- Observado nos prototypes: PowerShell/CIM/cmdlets nativos foram suficientes para o bundle inicial; nenhum P/Invoke ou vendor API foi necessário para esses dois prototypes.
- Observado nos prototypes adicionais: P/Invoke funcionou para identidade nativa; Registry HKCU permitiu snapshot/rollback/idempotência; Performance Counters exigiram descoberta localizada; ADL export exists para AMD neste host; admin/UAC produziu access denied estruturado; restart de processo controlado funcionou; WPR/ETW exigiu privilégio para captura.
- Observado: 20 domínios puderam emitir pelo menos um probe read-only estruturado; isso não demonstra cobertura completa de qualquer domínio.
- Observado: 1 de 27 probes do bundle exigiu elevação (DISM /CheckHealth); nos adicionais, WPR trace também falhou por requisito de privilégio.
- Observado: nenhuma prova executou reboot/logoff; restart de processo controlado e descoberta de lock owner via Restart Manager foram testados.
- Provas adicionais executadas:
  - `C-SYSTEM-001`: Win32/PInvoke GetNativeSystemInfo/RtlGetVersion PASS. Evidência: `/prototypes/system/C-SYSTEM-001/results/native-environment.json`.
  - `C-SYSTEM-007`: Registry HKCU baseline/diff/provenance PASS; fixture snapshot/apply/rollback is harness evidence, not feature Apply. Evidência: `/prototypes/system/C-SYSTEM-007/results/drift.json`.
  - `C-CPU-003`: Performance Counter localized discovery/sample PASS; English Get-Counter path FAIL on localized Windows. Evidência: `/prototypes/cpu/C-CPU-003/results/performance-counter-proof.json`.
  - `C-CPU-003`: PdhAddEnglishCounterW via P/Invoke sampled CPU successfully independent of UI language. Evidência: `/prototypes/cpu/C-CPU-003/results/pdh-samples.json`.
  - `C-CPU-003`: CPU WPR/ETW attempt executed and classified BLOCKED_ADMIN (0xc5585011). Evidência: `/prototypes/cpu/C-CPU-003/results/etw-attempt.json`.
  - `C-SYSTEM-012`: WPR present/status/profiles PASS; trace start FAIL in non-elevated session with 0xc5585011. Evidência: `/prototypes/system/C-SYSTEM-012/results/etw-wpr-proof.json`.
  - `C-GPU-015`: AMD ADL context create/enumerate/destroy PASS read-only; other vendors and tuning untested. Evidência: `/prototypes/gpu/C-GPU-015/results/amd-adl.json`.
  - `C-SECURITY-006`: Non-elevated token and admin-required/access-denied signal PASS; identity/group data redacted. Evidência: `/prototypes/security/C-SECURITY-006/results/admin-uac-proof.json`.
  - `C-PROCESSES-007`: Controlled child process dry-run/snapshot/terminate/restart/verify/cleanup PASS. Evidência: `/prototypes/processes/C-PROCESSES-007/results/process-restart.json`.
  - `C-STARTUP-001`: Startup Registry Run fixture inventory/provenance PASS with exact restoration in finally. Evidência: `/prototypes/startup/C-STARTUP-001/results/startup-inventory.json`.
- Inventário de resultados JSON persistidos:
  - `//dev/Projetos/beadwise/prototypes/audio/C-AUDIO-001/results/core-audio-proof.json`
  - `//dev/Projetos/beadwise/prototypes/cleaning/C-CLEANING-007/results/cleanup-sandbox-proof.json`
  - `//dev/Projetos/beadwise/prototypes/cleaning/C-CLEANING-009/results/multi-volume.json`
  - `//dev/Projetos/beadwise/prototypes/cpu/C-CPU-001/results/cpu-safe-capabilities.json`
  - `//dev/Projetos/beadwise/prototypes/cpu/C-CPU-003/results/etw-attempt.json`
  - `//dev/Projetos/beadwise/prototypes/cpu/C-CPU-003/results/pdh-samples.json`
  - `//dev/Projetos/beadwise/prototypes/cpu/C-CPU-003/results/performance-counter-proof.json`
  - `//dev/Projetos/beadwise/prototypes/display/C-DISPLAY-002/results/display-native-proof.json`
  - `//dev/Projetos/beadwise/prototypes/drivers/C-DRIVERS-001/results/driver-readonly-proof.json`
  - `//dev/Projetos/beadwise/prototypes/energy/C-ENERGY-013/results/timer-resolution-proof.json`
  - `//dev/Projetos/beadwise/prototypes/gpu/C-GPU-001/results/gpu-dxgi-proof.json`
  - `//dev/Projetos/beadwise/prototypes/gpu/C-GPU-015/results/amd-adl.json`
  - `//dev/Projetos/beadwise/prototypes/gpu/C-GPU-015/results/vendor-api-proof.json`
  - `//dev/Projetos/beadwise/prototypes/hardware/C-HARDWARE-001/results/hardware-proof.json`
  - `//dev/Projetos/beadwise/prototypes/network/C-NETWORK-001/results/network-proof.json`
  - `//dev/Projetos/beadwise/prototypes/privacy/C-PRIVACY-001/results/privacy-proof.json`
  - `//dev/Projetos/beadwise/prototypes/processes/C-PROCESSES-001/results/process-safe-capabilities.json`
  - `//dev/Projetos/beadwise/prototypes/processes/C-PROCESSES-007/results/process-restart-proof.json`
  - `//dev/Projetos/beadwise/prototypes/processes/C-PROCESSES-007/results/process-restart.json`
  - `//dev/Projetos/beadwise/prototypes/ram/C-RAM-004/results/ram-proof.json`
  - `//dev/Projetos/beadwise/prototypes/repair/C-REPAIR-006/results/repair-proof.json`
  - `//dev/Projetos/beadwise/prototypes/security/C-SECURITY-001/results/security-readonly.json`
  - `//dev/Projetos/beadwise/prototypes/security/C-SECURITY-006/results/admin-uac-proof.json`
  - `//dev/Projetos/beadwise/prototypes/startup/C-STARTUP-001/results/startup-inventory.json`
  - `//dev/Projetos/beadwise/prototypes/startup/C-STARTUP-006/results/startup-contract.json`
  - `//dev/Projetos/beadwise/prototypes/storage/C-STORAGE-001/results/storage-proof.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-001/results/environment.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-001/results/native-environment.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-007/results/baseline.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-007/results/current.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-007/results/drift.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-007/results/registry-drift-proof.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-011/results/system-safe-proof.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-012/results/etw-wpr-proof.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-017/results/domain-evidence-export.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-017/results/domain-evidence-export.manifest.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-017/results/final-verification.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-017/results/specified-audit-overrides.json`
  - `//dev/Projetos/beadwise/prototypes/system/C-SYSTEM-018/results/restart-manager-proof.json`
  - `//dev/Projetos/beadwise/prototypes/update/C-UPDATE-001/results/update-proof.json`
  - `//dev/Projetos/beadwise/prototypes/usb/C-USB-001/results/usb-proof.json`

### Cobertura representativa dos mecanismos

| Mecanismo | Resultado observado | Limite preservado |
|---|---|---|
| Win32 / PInvoke | PASS — GetNativeSystemInfo/RtlGetVersion e PDH; AMD ADL classic via interop. | Um host x64; ARM64 e outros vendors não testados. |
| WMI / CIM | PASS — bundle de 20 domínios e inventários nativos. | Sinais parciais; CIM não substitui API autoritativa específica. |
| Registry | PASS — leitura de policy/reboot e fixture HKCU de baseline/diff. | Nenhuma mutação HKLM/policy real. |
| PowerShell / CLI | PASS — cmdlets nativos, powercfg, net, DISM e WPR foram realmente invocados. | DISM e WPR preservam falhas de permissão. |
| Admin / UAC | PASS para detecção de token não elevado e sinal ACCESS_DENIED; caminho elevado BLOCKED. | Nenhum prompt UAC/helper elevado/IPC foi executado. |
| Hardware / vendor API | PASS somente para AMD ADL classic read-only. | Intel/NVIDIA, ADLX, tuning e matriz de hardware BLOCKED/NOT_TESTED. |
| Performance Counters | PASS — Get-Counter localizado e PdhAddEnglishCounterW temporal. | Thresholds/per-core/overhead amplo ainda limitados. |
| ETW / WPR | FAIL/BLOCKED_ADMIN — 0xc5585011 em duas tentativas não elevadas. | Nenhum ETL foi criado; falha não foi promovida. |
| Snapshot / Rollback | PASS em fixture HKCU e processo-filho próprios. | Evidência de harness; não implica Apply/Rollback em feature read-only. |
| Restart / Restart Manager | PASS para processo próprio controlado; `RmStartSession/RmRegisterResources/RmGetList` identificaram o lock owner e o restart foi verificado. | `RmShutdown/RmRestart`, serviço, dispositivo, logoff e reboot permaneceram bloqueados por risco/ambiente. |

## Revisão cruzada por domínio

- `audio`: 12 specs (DEFERRED=5, RESEARCH=2, SPECIFIED=5); prova segura explícita em 5/5 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `cleaning`: 9 specs (DEFERRED=1, RESEARCH=2, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `cpu`: 13 specs (BLOCKED=6, RESEARCH=1, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `display`: 12 specs (BLOCKED=4, DEFERRED=2, RESEARCH=4, SPECIFIED=2); prova segura explícita em 2/2 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `drivers`: 9 specs (BLOCKED=3, RESEARCH=1, SPECIFIED=5); prova segura explícita em 5/5 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `energy`: 11 specs (BLOCKED=2, DEFERRED=1, RESEARCH=7, SPECIFIED=1); prova segura explícita em 1/1 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `gaming`: 13 specs (BLOCKED=4, DEFERRED=2, RESEARCH=7); prova segura explícita em 0/0 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `gpu`: 15 specs (BLOCKED=1, DEFERRED=2, RESEARCH=9, SPECIFIED=3); prova segura explícita em 3/3 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `hardware`: 9 specs (BLOCKED=2, RESEARCH=4, SPECIFIED=3); prova segura explícita em 3/3 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `network`: 15 specs (BLOCKED=1, RESEARCH=8, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `privacy`: 6 specs (RESEARCH=4, SPECIFIED=2); prova segura explícita em 2/2 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `processes`: 7 specs (RESEARCH=1, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `ram`: 17 specs (BLOCKED=5, DEFERRED=1, RESEARCH=2, SPECIFIED=9); prova segura explícita em 9/9 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `repair`: 10 specs (BLOCKED=5, RESEARCH=1, SPECIFIED=4); prova segura explícita em 4/4 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `security`: 9 specs (BLOCKED=2, RESEARCH=4, SPECIFIED=3); prova segura explícita em 3/3 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `startup`: 8 specs (BLOCKED=1, RESEARCH=1, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `storage`: 21 specs (BLOCKED=7, DEFERRED=2, RESEARCH=6, SPECIFIED=6); prova segura explícita em 6/6 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `system`: 14 specs (BLOCKED=3, RESEARCH=3, SPECIFIED=8); prova segura explícita em 8/8 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `update`: 10 specs (RESEARCH=5, SPECIFIED=5); prova segura explícita em 5/5 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.
- `usb`: 14 specs (BLOCKED=9, RESEARCH=1, SPECIFIED=4); prova segura explícita em 4/4 `SPECIFIED`; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED.

## Revisão cruzada global

- Nenhum status PROVEN/APPROVED existe sem evidência.
- As 234 specs possuem ID único e distribuição final fechada; as 90 `SPECIFIED` têm `SAFE_PROOF` diferente de `NOT_TESTED` (90/90).
- Verificação central persistida: `PASS`; 40/40 harnesses executados, 0 falhas de harness, 0 ocorrências de privacidade e 234 specs. Evidência: `/prototypes/system/C-SYSTEM-017/results/final-verification.json`.
- O bundle comum registra provenance por probe, mas não foi usado como substituto de fonte de verdade específica.
- Permanecem abertos: ownership de inventários compartilhados fora do domínio System; fonte autoritativa por campo; semântica uniforme de Admin/Restart/System Restore; sobreposições Gaming↔Display/Audio/Network, GPU↔Processes/RAM e Network↔Security/Energy.
- As mutações destrutivas/alto risco não foram improvisadas no Windows real; seus resultados permanecem NOT_TESTED/RESEARCH conforme a spec.
- Nenhum código do prototype foi promovido para backend final, solution, UI, DI, IPC ou contratos finais.
