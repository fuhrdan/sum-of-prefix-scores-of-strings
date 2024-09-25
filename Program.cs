//*****************************************************************************
//** 2416. Sum of Prefix Scores of Strings   leetcode                        **
//*****************************************************************************


/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
#define ALPHABET_SIZE 26

// Define the Trie Node structure
typedef struct TrieNode {
    struct TrieNode* children[ALPHABET_SIZE]; // Array to store child nodes for each letter
    int prefixCount;                          // Count of words passing through this node
} TrieNode;

// Function to create a new TrieNode
TrieNode* createNode() {
    TrieNode* newNode = (TrieNode*)malloc(sizeof(TrieNode));
    newNode->prefixCount = 0;

    // Initialize all children as NULL
    for (int i = 0; i < ALPHABET_SIZE; i++) {
        newNode->children[i] = NULL;
    }

    return newNode;
}

// Trie structure for storing string representations
typedef struct Trie {
    TrieNode* root;
} Trie;

// Function to initialize a Trie
Trie* createTrie() {
    Trie* trie = (Trie*)malloc(sizeof(Trie));
    trie->root = createNode();
    return trie;
}

// Insert a string into the Trie and update the prefix counts
void insert(Trie* trie, const char* word) {
    TrieNode* node = trie->root;

    for (int i = 0; word[i] != '\0'; i++) {
        int index = word[i] - 'a';

        // If the character is not already in the Trie, add a new node
        if (node->children[index] == NULL) {
            node->children[index] = createNode();
        }

        node = node->children[index];
        node->prefixCount++;  // Increment the prefix count for this node
    }
}

// Compute the prefix score for a word
int computePrefixScore(Trie* trie, const char* word) {
    TrieNode* node = trie->root;
    int score = 0;

    for (int i = 0; word[i] != '\0'; i++) {
        int index = word[i] - 'a';

        if (node->children[index] == NULL) {
            break;  // If the prefix doesn't exist, stop
        }

        node = node->children[index];
        score += node->prefixCount;  // Add the count of words passing through this node
    }

    return score;
}

// The main function to solve the problem
int* sumPrefixScores(char** words, int wordsSize, int* returnSize) {
    Trie* trie = createTrie();
    int* result = (int*)malloc(wordsSize * sizeof(int));

    *returnSize = wordsSize;

    // Step 1: Insert all words into the Trie
    for (int i = 0; i < wordsSize; i++) {
        insert(trie, words[i]);
    }

    // Step 2: Calculate prefix scores for each word
    for (int i = 0; i < wordsSize; i++) {
        result[i] = computePrefixScore(trie, words[i]);
    }

    return result;
}
