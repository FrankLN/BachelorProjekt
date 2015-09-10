#pragma once
template<class T>
class SharedPtr
{
public:
	SharedPtr(T* t = 0) : t_(t) { t_count = 1; };
	SharedPtr(const SharedPtr& sP) : t_(sP) { t_count++;  };
	SharedPtr &operator=(const SharedPtr& sP) { t_ = sP; return *this; };
	~SharedPtr() { if (t_ && --t_count <= 0) { delete t_; std::cout << "deleted" << std::endl; } else std::cout << "not deleted" << std::endl; };

	T& operator*() const  { return t_*; }
	T* operator->() const { return t_; }
private:
	T* t_;
	int t_count;
};

