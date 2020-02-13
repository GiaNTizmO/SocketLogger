package ru.gamania.logger.sockets;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.util.List;

public class ClientSocket {
    private final InetAddress inetAddress;
    private final int port;
    private Socket socket;
    private List<String> lines;
    private int lastSize;

    public ClientSocket(InetAddress inetAddress, int port) throws IOException {
        this.inetAddress = inetAddress;
        this.port = port;

        this.setConnection();
    }

    public void writeList(List<String> lines) {
        this.lines = lines;
    }

    public boolean hasNewLines() {
        return this.lastSize != this.lines.size();
    }

    public synchronized void writeSocket() throws IOException {
        if (this.socket.isClosed()) this.setConnection();
        if (this.socket.isConnected())
            try (OutputStream outputStream = this.socket.getOutputStream()) {
                DataOutputStream dataOutputStream = new DataOutputStream(outputStream);

                dataOutputStream.writeInt(this.lines.size() - this.lastSize);

                for (; this.lastSize < this.lines.size(); this.lastSize++)
                    dataOutputStream.writeUTF(this.lines.get(this.lastSize));

                outputStream.flush();
            } catch (IOException e) {
                e.printStackTrace();
            }
    }

    private void setConnection() throws IOException {
        this.socket = new Socket(inetAddress, port);

        this.socket.setKeepAlive(true);
    }
}
