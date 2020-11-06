import { ChatMessageDto } from 'MyModels';
import { events } from 'src/core-hub';
import { onEventOccurred, subscribeEvent } from 'src/store/signal/actions';

export const subscribeChatMessages = () => subscribeEvent(events.chatMessage);

export const onChatMessage = onEventOccurred<ChatMessageDto>(events.chatMessage);