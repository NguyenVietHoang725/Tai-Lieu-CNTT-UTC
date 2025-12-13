// BinarySearchTree.h
#ifndef BINARYSEARCHTREE_H
#define BINARYSEARCHTREE_H

#include <iostream>
using namespace std;

// Định nghĩa một nút trong cây
template <typename T>
struct Node {
    T data;
    Node* left;
    Node* right;

    Node(T value) : data(value), left(nullptr), right(nullptr) {}
};

// Lớp cài đặt cây tìm kiếm nhị phân (BST) sử dụng template
template <typename T>
class BinarySearchTree {
public:
    Node<T>* root;

    BinarySearchTree() : root(nullptr) {}

    ~BinarySearchTree() {
        xoaCay();
    }

    void them(T value) {
        root = themNode(root, value);
    }

    Node<T>* timKiem(T value) {
        return timNode(root, value);
    }

    void duyetGiua() {
        duyetGiuaNode(root);
        cout << endl;
    }

    void duyetTruoc() {
        duyetTruocNode(root);
        cout << endl;
    }

    void duyetSau() {
        duyetSauNode(root);
        cout << endl;
    }

    void xoa(T value) {
        root = xoaNode(root, value);
    }

    bool laRong() const {
        return root == nullptr;
    }

    void xoaCay() {
        xoaToanBo(root);
        root = nullptr;
    }

    int demNode() {
        return demNodeHelper(root);
    }

    int chieuCao() {
        return chieuCaoHelper(root);
    }

private:
    Node<T>* themNode(Node<T>* node, T value) {
        if (node == nullptr) {
            return new Node<T>(value);
        }
        if (value < node->data) {
            node->left = themNode(node->left, value);
        } else if (value > node->data) {
            node->right = themNode(node->right, value);
        }
        return node;
    }

    Node<T>* timNode(Node<T>* node, T value) {
        if (node == nullptr) return nullptr; 
        if (node->data == value) return node; 
        if (value < node->data) {
            return timNode(node->left, value); 
        } else {
            return timNode(node->right, value); 
        }
    }

    void duyetGiuaNode(Node<T>* node) {
        if (node != nullptr) {
            duyetGiuaNode(node->left);
            cout << node->data;
            duyetGiuaNode(node->right);
        }
    }

    void duyetTruocNode(Node<T>* node) {
        if (node != nullptr) {
            cout << node->data;
            duyetTruocNode(node->left);
            duyetTruocNode(node->right);
        }
    }

    void duyetSauNode(Node<T>* node) {
        if (node != nullptr) {
            duyetSauNode(node->left);
            duyetSauNode(node->right);
            cout << node->data;
        }
    }

    Node<T>* timMin(Node<T>* node) {
        while (node->left != nullptr) {
            node = node->left;
        }
        return node;
    }

    Node<T>* xoaNode(Node<T>* node, T value) {
        if (node == nullptr) return node;

        if (value < node->data) {
            node->left = xoaNode(node->left, value);
        } else if (value > node->data) {
            node->right = xoaNode(node->right, value);
        } else {
            if (node->left == nullptr) {
                Node<T>* temp = node->right;
                delete node;
                return temp;
            } else if (node->right == nullptr) {
                Node<T>* temp = node->left;
                delete node;
                return temp;
            }

            Node<T>* temp = timMin(node->right);
            node->data = temp->data;
            node->right = xoaNode(node->right, temp->data);
        }
        return node;
    }

    void xoaToanBo(Node<T>* node) {
        if (node != nullptr) {
            xoaToanBo(node->left);
            xoaToanBo(node->right);
            delete node;
        }
    }

    int demNodeHelper(Node<T>* node) {
        if (node == nullptr) return 0;
        return 1 + demNodeHelper(node->left) + demNodeHelper(node->right);
    }

    int chieuCaoHelper(Node<T>* node) {
        if (node == nullptr) return 0;
        return 1 + max(chieuCaoHelper(node->left), chieuCaoHelper(node->right));
    }
};

#endif
