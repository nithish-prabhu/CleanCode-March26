// Define Interfaces
interface INotificationChannel {
    void sendToUser(String content){};
}


//Implement Notification Channels
class EmailChannel implements INotificationChannel {
    @Override
    public void sendToUser(String content) {
        System.out.println("Sending Email: " + content);
    }
}

class SMSChannel implements INotificationChannel {
    @Override
    public void sendToUser(String content) {
        System.out.println("Sending SMS: " + content);
    }
}

class PushNotificationChannel implements INotificationChannel {
    @Override
    public void sendToUser(String content) {
        System.out.println("Sending Push Notification: " + content);
    }
}

// Step 3: Implement Notification Content Creation
class NotificationContentFactory {
    public static String createContent(String message, String channelType) {
        switch (channelType) {
            case "email":
                return "<html><body>" + message + "</body></html>"; 
            case "sms":
                return message.substring(0, Math.min(message.length(), 160)); 
            case "push":
                return message.substring(0, Math.min(message.length(), 100)); 
            //More channels can be added here
            default:
                throw new IllegalArgumentException("Unsupported channel type: " + channelType);
        }
    }
}

// Step 4: Create Notification Service
class NotificationService {
    private final INotificationChannel channel;

    public NotificationService(INotificationChannel channel) {
        this.channel = channel;
    }

    public void sendNotification(String message, String channelType) {
        String content = NotificationContentFactory.createContent(message, channelType);
        channel.sendToUser(content);
    }
}

// Step 5: Use a Registry for Channels
class ChannelRegistry {
    private final Map<String, INotificationChannel> channels = new HashMap<>();

    public void registerChannel(String name, INotificationChannel channel) {
        channels.put(name, channel);
    }

    public INotificationChannel getChannel(String name) {
        return channels.get(name);
    }
}

public class Main {
    public static void main(String[] args) {
        // Register available channels
        ChannelRegistry registry = new ChannelRegistry();
        registry.registerChannel("email", new EmailChannel());
        registry.registerChannel("sms", new SMSChannel());
        registry.registerChannel("push", new PushNotificationChannel());
        //Add new channels here

        // Get user preference 
        String userPreference = "email";

        // Get the appropriate channel from the registry
        INotificationChannel channel = registry.getChannel(userPreference);

        if (channel != null) {
            // Create and use NotificationService
            NotificationService service = new NotificationService(channel);
            service.sendNotification("Notification sent succesfully via", userPreference);
        } 
        else {
            System.out.println("Unsupported notification preference: " + userPreference);
        }
    }
}
