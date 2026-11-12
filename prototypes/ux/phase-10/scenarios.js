export const scenarios = {
  success: "Sucesso completo",
  empty: "Sem recomendações",
  unavailable: "Dados indisponíveis",
  admin: "Permissão necessária",
  partial: "Falha parcial",
  restart: "Reinício pendente",
  recovery: "Recuperação disponível",
  recoveryFailed: "Falha na recuperação",
};
export const features = [
  {
    id: "energy",
    title: "Revisar suspensão de dispositivos USB",
    category: "Energia",
    reason:
      "Neste exemplo, um dispositivo apresenta desconexões após períodos de inatividade.",
    benefit:
      "Pode evitar interrupções neste dispositivo. O consumo de energia pode aumentar.",
    current: "Suspensão ativada",
    target: "Suspensão desativada",
    risk: "Medium",
    privilege: "Administrator",
    restart: "Device",
    reversibility: "Reversible",
  },
  {
    id: "startup",
    title: "Reduzir aplicativos na inicialização",
    category: "Inicialização",
    reason:
      "Um aplicativo opcional está configurado para abrir junto com o Windows.",
    benefit: "O aplicativo continuará disponível para abertura manual.",
    current: "Abertura automática ativada",
    target: "Abertura automática desativada",
    risk: "Low",
    privilege: "None",
    restart: "None",
    reversibility: "Reversible",
  },
  {
    id: "cleanup",
    title: "Remover arquivos temporários de demonstração",
    category: "Armazenamento",
    reason:
      "Arquivos temporários elegíveis foram encontrados no cenário simulado.",
    benefit:
      "Libera o espaço desses arquivos. Arquivos removidos não podem ser recuperados por este fluxo.",
    current: "Arquivos presentes",
    target: "Arquivos removidos",
    risk: "Medium",
    privilege: "None",
    restart: "None",
    reversibility: "Irreversible",
  },
  {
    id: "unsupported",
    title: "Ajustar recurso específico do fabricante",
    category: "Hardware",
    reason: "O equipamento deste exemplo não oferece essa capacidade.",
    status: "Unsupported",
  },
  {
    id: "applied",
    title: "Manter proteção do sistema",
    category: "Sistema",
    reason: "A configuração observada já corresponde ao estado recomendado.",
    status: "AlreadyApplied",
  },
];
export const labels = {
  Low: "Baixo",
  Medium: "Moderado",
  Administrator: "Administrador",
  None: "Não necessário",
  Device: "Reconectar dispositivo",
  Reboot: "Reiniciar Windows",
  Reversible: "Pode desfazer",
  Irreversible: "Não pode desfazer",
};
