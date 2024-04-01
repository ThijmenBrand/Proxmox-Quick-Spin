interface Node {
  node: string;
  status: string;
  cpu?: number;
  level?: string;
  maxcpu?: number;
  maxmem?: number;
  mem?: number;
  ssl_fingerprint?: string;
  uptime?: number;
}
