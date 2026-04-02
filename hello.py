from typing import Any
import sys
import logging
from mcp.server.fastmcp import FastMCP

# Reduce MCP SDK logging verbosity
logging.getLogger("mcp").setLevel(logging.WARNING)

# Initialize FastMCP server
mcp = FastMCP("weather", stateless_http=True)

# Add a simple tool to demonstrate the server
@mcp.tool()
def greet(name: str = "World") -> str:
    """Greet someone by name."""
    return f"Hello, {name}!"
  
if __name__ == "__main__":
    try:
        # Initialize and run the server
        print("Starting MCP server...")
        mcp.run(transport="streamable-http")
    except Exception as e:
        print(f"Error while running MCP server: {e}", file=sys.stderr)