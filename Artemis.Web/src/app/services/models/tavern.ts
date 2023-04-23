export interface TavernDescription {
    description: string;
    options: TavernModelOption[];
}

export interface ChatMessage {
    role: string;
    content: string;
}

export interface TavernModelOption {
    actionName: string;
    description: string;
}

export interface TavernModel {
    tavernModelDescription: TavernDescription;
    messages: ChatMessage[];
}

export interface NextAction {
    prompt: string;
    messages: ChatMessage[];
}

export interface Action {
    description: string;
    action: string;
}